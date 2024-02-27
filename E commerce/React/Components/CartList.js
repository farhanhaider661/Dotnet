import React, { Fragment, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
function CartList(){
    let navigate=useNavigate();
    const backToCart=()=>{
        navigate('/');
    };
    const[data, setData]=useState([]);
    useEffect(()=>{
        axios.get('https://localhost:7102/api/Shop/ProductList')
        .then((result)=>{
            setData(result.data);
            console.log("Response is: ",result);
        })
        .catch((error)=>{
            console.log(error);
        })
    },[]);
    const DeleteCart= async(id)=>{
        console.log(id);
        try{
       await axios.delete(`https://localhost:7102/api/Shop/${id}`);
        alert('Remove From Cart');  
       // Update the state to filter out the item that was deleted
       const newData = data.filter(item => item.id !== id);
       setData(newData);
     }
        catch (error) {
            console.error('Error deleting student:', error);
          }  
    }
return(
    <Fragment>
        
        <button className="btn btn-warning" onClick={backToCart}>Back</button>
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
        <button className="btn btn-danger"
         onClick={()=>DeleteCart(item.id)}
         >Remove Item</button>
        </div>
        </div>
            );
        })}
    </div>
    ):("No Data")}
    </Fragment>
);
}
export default CartList;