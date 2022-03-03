import logo from './logo.svg';
import './App.css';
import './components/Login/login.css'
import "bootstrap/dist/css/bootstrap.min.css";
import 'react-toastify/dist/ReactToastify.css';
import Test from './components/Test';
import {React,Fragment,useState} from 'react';
import { ToastContainer, toast } from 'react-toastify';
import { BrowserRouter, Routes, Route,  Navigate } from "react-router-dom";
import Login from './components/Login';

function App() {
  return (
  
    <Fragment>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to={"/login"}> </Navigate>}></Route>
        <Route path="*" element={<Navigate to={"/login"}> </Navigate>}></Route>

        <Route path="/login" element={<Login></Login>} ></Route>
{/* 
        <Route path="/admin/dashboard" element={<Dashboard></Dashboard>} ></Route>
        <Route path="/admin/order" element={<AdminOrder></AdminOrder>} ></Route>
        <Route path="/admin/product" element={<AdminProduct></AdminProduct>} ></Route>
        <Route path="/admin/promotion" element={<AdminPromotion></AdminPromotion>} ></Route>
        <Route path="/admin/user" element={<AdminUser></AdminUser>} ></Route>
        <Route path="/admin/employee" element={<AdminEmp></AdminEmp>} ></Route>
        <Route path="/admin/discount" element={<AdminDiscount></AdminDiscount>} ></Route> */}

      </Routes>
      <ToastContainer></ToastContainer>
    </BrowserRouter>
 
  </Fragment>
  );
}

export default App;
