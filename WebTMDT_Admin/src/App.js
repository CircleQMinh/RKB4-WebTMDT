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
import AdminDashboard from './components/Admin/AdminDashboard';
import AdminProduct from './components/Admin/AdminProduct/Index';

function App() {
  return (
  
    <Fragment>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to={"/login"}> </Navigate>}></Route>
        <Route path="*" element={<Navigate to={"/login"}> </Navigate>}></Route>

        <Route path="/login" element={<Login></Login>} ></Route>

        <Route path="/admin/dashboard" element={<AdminDashboard></AdminDashboard>} ></Route>

        <Route path="/admin/product" element={<AdminProduct></AdminProduct>} ></Route>


      </Routes>
      <ToastContainer></ToastContainer>
    </BrowserRouter>
 
  </Fragment>
  );
}

export default App;
