import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Cart from './Components/Cart';
import CartList from './Components/CartList';

function App() {
  return (
    <Router>
    <div className="App">
      <Routes>
        <Route path='/' element={<Cart/>}/>
        <Route path='/cart' element={<CartList/>}/>
      </Routes>
    </div>
    </Router>
  );
}

export default App;
