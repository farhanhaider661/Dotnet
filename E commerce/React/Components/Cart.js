import React, { Fragment, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
function Cart(){
    let navigate=useNavigate();
    const goToCart=()=>{
        navigate('/cart');
    };
    const[data, setData]=useState([]);
    useEffect(()=>{
        axios.get('https://localhost:7102/api/Shop')
        .then((result)=>{
            setData(result.data);
            console.log("Response is: ",result);
        })
        .catch((error)=>{
            console.log(error);
        })
    },[]);
    const handleAddProduct=(id)=>{
        console.log(id);
        const data={
            productId:id
        };
        axios.post('https://localhost:7102/api/Shop',data)
        .then((result)=>{
                alert('Item Added');
                console.log(result);
        }).catch((error)=>{
            console.log(error);
        });
    }
return (
    <Fragment>
        
        <button className="btn btn-warning" onClick={goToCart}>Open Cart</button>
    {
        data && data.length>0 ?(
    <div className='row'>
        {data.map((item,index)=>{
            {console.log(item)}
            const imgPath=`assets/${item.productImage.trim()}`;
            return(
        <div className="card col-4">
        <img src={imgPath} style={{height:'373px',width:'522px'}} alt="HeadPhones"/>
        <div className="card-body">
        <h5 className="card-title">{item.productName}</h5>
        <div style={{ display: 'flex', gap: '0.5rem', alignItems: 'center' }}>
        <h3 style={{ margin: 0 }}>${item.discountPrice}</h3>
        <h3 style={{ margin: 0, textDecoration: 'line-through', color:'red' }}>${item.actualPrice}</h3>
        </div>
        <button className="btn btn-success" onClick={()=>handleAddProduct(item.id)}>Add To Cart</button>
        </div>
        </div>
            );
        })}
    </div>
    ):("No Data")}
    </Fragment>
);
}
export default Cart;