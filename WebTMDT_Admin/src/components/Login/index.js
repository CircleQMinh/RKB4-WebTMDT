import React, { Fragment, useEffect, useState } from "react";
import AuthService from "../../api/AuthService";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { auth_action } from "../../redux/auth_slice.js";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

function Login() {
  //form login
  let {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm();

  //var
  const dispatch = useDispatch();
  const isLoggedIn = useSelector((state) => state.auth_slice.isLoggedIn);
  var [isSubmitting, setIsSubmitting] = useState(false);
  const user = useSelector((state) => state.auth_slice.user);
  const navigate = useNavigate();

  //function
  async function TryLogin(data) {
    setIsSubmitting(true);
    AuthService.Login(data.email, data.password)
      .then((response) => {
        //console.log(response);
        if (response.data.success) {
          toast.success("Đăng nhập thành công!", {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
          dispatch(auth_action.saveAuthInfoToLocalStorage(response.data));
          var redirect = localStorage.getItem("redirect");
          if (redirect) {
            localStorage.removeItem("redirect");
            navigate(redirect);
          }
          window.location.reload();
        } else {
          toast.error(response.data.msg, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
        }
      })
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        setIsSubmitting(false);
      });
  }

  useEffect(() => {
    dispatch(auth_action.getAuthInfoFromLocalStorage());
  }, [isLoggedIn, dispatch]);

  return (
    <div className="admin_page_body">
      <section className="ftco-section">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-6 text-center mb-5">
              <h2 className="heading-section">
                {" "}
                <img
                  src="https://res.cloudinary.com/dkmk9tdwx/image/upload/v1628192627/logo_v5ukvv.png"
                  className="brand_logo me-2"
                  alt="Logo"
                />
                Circle's Shop Administration
              </h2>
            </div>
          </div>
          <div className="row justify-content-center">
            <div className="col-md-6 col-lg-4">
              <div className="login-wrap p-0">
                <h3 className="mb-4 text-center">Nhập thông tin đăng nhập</h3>
                <form className="signin-form">
                  <div className="form-group mb-3">
                    <input
                      type="text"
                      className="form-control mb-3"
                      placeholder="Email"
                      required=""
                      {...register("email", {
                        required: true,
                        pattern:
                          /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                      })}
                    ></input>
                    {errors.email?.type === "required" && (
                      <p className="text-center">
                        <i className="fas fa-exclamation-triangle"></i>Email
                        không để trống
                      </p>
                    )}
                    {errors.email?.type === "pattern" && (
                      <p className="text-center">
                        <i className="fas fa-exclamation-triangle"></i>Email
                        không hợp lệ!
                      </p>
                    )}
                  </div>
                  <div className="form-group mb-3">
                    <input
                      id="password-field"
                      type="password"
                      className="form-control"
                      placeholder="Password"
                      required=""
                      {...register("password", {
                        required: true,
                      })}
                    ></input>
                    {errors.password?.type === "required" && (
                      <p className="text-center">
                        <i className="fas fa-exclamation-triangle"></i>Password
                        không để trống
                      </p>
                    )}
                  </div>
                  <div className="form-group mb-3">
                  <button
                      type="submit"
                      name="button"
                      className={"btn login_btn"}
                      disabled={isSubmitting}
                    >
                      {isSubmitting && (
                        <div className="spinner-border" role="status">
                          <span className="visually-hidden">Loading...</span>
                        </div>
                      )}
                      {!isSubmitting && (
                        <Fragment>
                          <i className="fas fa-sign-in-alt me-2"></i> Login{" "}
                        </Fragment>
                      )}
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Login;
