import React, { Fragment, useEffect, useState } from "react";
import {
  Navbar,
  Container,
  NavItem,
  Button,
  NavDropdown,
  Form,
  Nav,
} from "react-bootstrap";
import { NavLink } from "react-router-dom";
import "../Admin.css";
import { auth_action } from "../../../redux/auth_slice.js";
import AuthService from "../../../api/AuthService";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";


function AdminHeader(props) {

  var navigate = useNavigate()

  //dropdown
  const [show, setShow] = useState(false);
  const showDropdown = (e) => {
    setShow(!show);
  };
  const hideDropdown = (e) => {
    setShow(false);
  };

  const [show_profile, setShow_profile] = useState(false);
  const showDropdown_profile = (e) => {
    setShow_profile(!show);
  };
  const hideDropdown_profile = (e) => {
    setShow_profile(false);
  };

  //var
  const dispatch = useDispatch();
  const isLoggedIn = useSelector((state) => state.auth_slice.isLoggedIn);
  const user = useSelector((state) => state.auth_slice.user);

  //function
  function LogOut() {
    dispatch(auth_action.logOut());
    navigate("/home")
  }

  //run first
  useEffect(() => {
    dispatch(auth_action.getAuthInfoFromLocalStorage());
  }, [isLoggedIn, dispatch]);

  // for (const [key, value] of Object.entries(user)) {
  //   console.log(`${key}: ${value}`);
  // }

  return (
    <Fragment>
      <Navbar
        expand="lg"
        variant="dark"
        sticky="top"
        className="p-0 me-auto"
        style={{ backgroundColor: "#27293D" }}
      >
        <Navbar.Brand as={NavLink} to={"/admin/dashboard"} className="ms-3">
          <p className={"show_for_991"} style={{ display: "inline" }}>
            <i className="fas fa-home me-2" ></i> Dashboard
          </p>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />

        <Navbar.Collapse id="basic-navbar-nav" className="">
          <ul className="navbar-nav mx-auto ">
            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/order"}>
                <i className="fas fa-file-invoice-dollar"></i>
                <p className="d-inline show_for_991"> Đơn hàng</p>
              </Nav.Link>
            </NavItem>
            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/product"}>
                <i className="fas fa-shopping-bag"></i>
                <p className="d-inline show_for_991"> Sản phẩm</p>
              </Nav.Link>
            </NavItem>

            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/employee"}>
                <i className="far fa-id-card"></i>
                <p className="d-inline show_for_991"> Nhân viên</p>
              </Nav.Link>
            </NavItem>
            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/user"}>
                <i className="fa-solid fa-user"></i>
                <p className="d-inline show_for_991"> Người dùng</p>
              </Nav.Link>
            </NavItem>
            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/promotion"}>
                <i className="fas fa-percent"></i>
                <p className="d-inline show_for_991"> Khuyến mãi</p>
              </Nav.Link>
            </NavItem>
            <NavItem as="li">
              <Nav.Link as={NavLink} to={"/admin/discount"}>
                <i className="fas fa-qrcode"></i>
                <p className="d-inline show_for_991"> Giảm giá</p>
              </Nav.Link>
            </NavItem>
          </ul>
          <Nav as="ul" className="me-auto">
            {isLoggedIn && (
              <Fragment>
                <NavDropdown
                  show={show_profile}
                  onMouseEnter={showDropdown_profile}
                  onMouseLeave={hideDropdown_profile}
                  as="li"
                  className="drop_down_li"
                  title={
                    <div className="d-flex flex-row ">
                      <img
                        src={user.imgUrl}
                        alt="avatar"
                        className="avatar_icon "
                      ></img>
                      <div className="d-flex align-items-center pt-2 ps-1">
                        <p className="hide_for_1399 show_for_991 p-0 ">
                          {user.userName}{" "}
                        </p>
                      </div>
                    </div>
                  }
                  id="profile_drop"
                  style={{ marginRight: 30 + "px" }}
                >
                  <NavDropdown.Item as={NavLink}  to={`/profile/${user.id}`}>
                    <i className="fas fa-user me-2"></i>
                    <p className="d-inline show_for_991">Tài khoản</p>
                  </NavDropdown.Item>
                  <NavDropdown.Item as={NavLink}  to={`/home`}>
                  <i className="fas fa-home me-2" ></i>
                    <p className="d-inline show_for_991">Cửa hàng</p>
                  </NavDropdown.Item>
                  <NavDropdown.Divider />
                  <NavDropdown.Item onClick={LogOut}>
                    <i className="fas fa-sign-in-alt  me-2"></i>
                    <p className="d-inline  show_for_991">Đăng xuất</p>
                  </NavDropdown.Item>
                </NavDropdown>
              </Fragment>
            )}
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    </Fragment>
  );
}

export default AdminHeader;
