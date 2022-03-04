import React from "react";
import { Link } from "react-router-dom";
import "./Footer.css";


function AdminFooter(props) {

  return (
    <footer className="site-footer ">
      <div className="container">
        <div className="row">
          <div className="col-sm-12 col-md-6">
            <h6>Về chúng tôi</h6>
            <p className="text-justify">
              Cửa hàng mua sách online TP.HCM. Chuyên bán các loại sách, văn học, tiểu thuyết và truyện tranh.
            </p>
          </div>

          <div className="col-xs-6 col-md-3">
            <h6>Dịch vụ</h6>
            <ul className="footer-links">
              <li>
                <Link to={"/search"}>Tìm sản phẩm</Link>
              </li>
              <li>
                <Link to={"/home"}>Mua sắm trực tuyến</Link>
              </li>
              <li>
                <Link to={"/home"}>Giao hàng miễn phí</Link>
              </li>
            </ul>
          </div>

          <div className="col-xs-6 col-md-3">
            <h6>Quick Links</h6>
            <ul className="footer-links">
              <li>
                <a href="https://github.com/CircleQMinh" >
                  My Github Page
                </a>
              </li>
            </ul>
          </div>
        </div>
        <hr />
      </div>
      <div className="container">
        <div className="row">
          <div className="col-md-8 col-sm-6 col-xs-12">
            <p className="copyright-text">
              Copyright &copy; 2021 All Rights Reserved by
              <a href="https://github.com/CircleQMinh">Quốc Minh</a>.
              (04/03/2021)
            </p>
          </div>

          <div className="col-md-4 col-sm-6 col-xs-12">
            <ul className="social-icons">
              <li>
                <Link className="facebook" to={"/home"}>
                  <i className="fa fa-facebook"></i>
                </Link>
              </li>
              <li>
                <Link className="twitter" to={"/home"}>
                  <i className="fa fa-twitter"></i>
                </Link>
              </li>
              <li>
                <Link className="dribbble" to={"/home"}>
                  <i className="fa fa-dribbble"></i>
                </Link>
              </li>
              <li>
                <Link className="linkedin" to={"/home"}>
                  <i className="fa fa-linkedin"></i>
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
  );
}

export default AdminFooter;
