import { React, Fragment, useEffect, useState } from "react";

import "../Admin.css";

import { auth_action } from "../../../redux/auth_slice.js";
import AuthService from "../../../api/AuthService";
import { useSelector, useDispatch } from "react-redux";
import { Link, NavLink, useNavigate } from "react-router-dom";
import AdminHeader from "../AdminHeader";
import AdminFooter from "../AdminFooter";
import { toast } from "react-toastify";
import Loading from "../../../shared-components/Loading";
import AdminService from "../../../api/AdminService";

function AdminDashboard() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [reRender, setReRender] = useState(true);
  const [authorizing, setAuthorizing] = useState(true);
  const [dashboardInfo, setDashboardInfo] = useState({
    orderCount: 0,
    productCount: 0,
    uncheckOrderCount: 0,
    userCount: 0,
  });
  useEffect(() => {
    AuthService.GetAuthorizeAdmin()
      .then((res) => {
        //console.log(res.data);
        LoadDashboardInfo().then(() => {
          setAuthorizing(false);
        });
      })
      .catch((e) => {
        toast.success("Xác thực không thành công! Xin hãy đăng nhập trước", {
          position: "top-center",
          autoClose: 2000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
        });
        setTimeout(() => {
          dispatch(auth_action.logOut());
          navigate("/login");
        }, 2500);
      })
      .finally(() => {});
  }, [reRender]);

  async function LoadDashboardInfo() {
    AdminService.GetDashboardInfo()
    .then((res) => {
      console.log(res.data);
      var data = res.data;
      var info = {
        orderCount: data.orderCount,
        productCount: data.productCount,
        uncheckOrderCount: data.uncheckOrderCount,
        userCount: data.userCount,
      };
      setDashboardInfo(info);
    })
    .catch((e) => {
      console.log(e);
    })
    .finally(() => {});
  }
  function ReRender() {
    setReRender(!reRender);
  }

  return (
    <Fragment>
      {!authorizing && (
        <Fragment>
          <AdminHeader></AdminHeader>
          <div className="w-100 h-100" style={{ backgroundColor: "#1E1E28" }}>
            <div className="container  py-3 ">
            <div className="row">
                <div className="col-sm-12 col-lg-6 mb-3">
                  <div className="card custom-card">
                    <div className="card-body d-flex">
                      <div className="col-10">
                        <div className="card-title mb-2">
                          <label className="tx-13 mb-1">Tổng số sản phẩm</label>
                        </div>
                        <h4 className="font-weight-normal">
                          {dashboardInfo.productCount}
                        </h4>{" "}
                        <small>
                          <Link
                            to={"/admin/product"}
                            className="btn btn-primary"
                          >
                            Đến quản lý sản phẩm{" "}
                            <i className="fa-solid fa-arrow-right ms-2"></i>
                          </Link>
                        </small>
                      </div>
                      <div className="col-2 fs-5">
                        <i className="fa-solid fa-book admin_db_icon"></i>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="col-sm-12 col-lg-6 mb-3">
                  <div className="card custom-card">
                    <div className="card-body d-flex">
                      <div className="col-10">
                        <div className="card-title mb-2">
                          <label className="tx-13 mb-1">
                            Đơn hàng chưa duyệt
                          </label>
                        </div>
                        <h4 className="font-weight-normal">
                          {dashboardInfo.uncheckOrderCount}
                        </h4>{" "}
                        <small>
                          <Link to={"/admin/order"} className="btn btn-success">
                            Đến quản lý đơn hàng
                            <i className="fa-solid fa-arrow-right ms-2"></i>
                          </Link>
                        </small>
                      </div>
                      <div className="col-2 fs-5">
                        <i className="fa-solid fa-file-invoice admin_db_icon"></i>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="col-sm-12 col-lg-6 mb-3">
                  <div className="card custom-card">
                    <div className="card-body d-flex">
                      <div className="col-10">
                        <div className="card-title mb-2">
                          <label className="tx-13 mb-1">
                            Tổng số người dùng
                          </label>
                        </div>
                        <h4 className="font-weight-normal">
                          {dashboardInfo.userCount}
                        </h4>{" "}
                        <small>
                          <Link to={"/admin/user"} className="btn btn-warning">
                            Đến quản lý user
                            <i className="fa-solid fa-arrow-right ms-2"></i>
                          </Link>
                        </small>
                      </div>
                      <div className="col-2 fs-5">
                        <i className="fas fa-user admin_db_icon"></i>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="col-sm-12 col-lg-6 mb-3">
                  <div className="card custom-card">
                    <div className="card-body d-flex">
                      <div className="col-10">
                        <div className="card-title mb-2">
                          <label className="tx-13 mb-1">
                            Xem chi tiết thống kê
                          </label>
                        </div>
                        <h4 className="font-weight-normal">
                          Thống kê chi tiết
                        </h4>{" "}
                        <small>
                          <Link
                            to={"/admin/statistic"}
                            className="btn btn-danger"
                          >
                            Xem thông kê
                            <i className="fa-solid fa-arrow-right ms-2"></i>
                          </Link>
                        </small>
                      </div>
                      <div className="col-2 fs-5">
                        <i className="fa-solid fa-chart-line admin_db_icon"></i>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="row">
                <div className="container" style={{ height: 500 + "px" }}>
                  <button className="btn btn-primary" onClick={ReRender}>
                    Update
                  </button>
   
                </div>
              </div>
            </div>
          </div>
          <AdminFooter></AdminFooter>
        </Fragment>
      )}
      {authorizing && (
        <Loading></Loading>
      )}
    </Fragment>
  );
}

export default AdminDashboard;
