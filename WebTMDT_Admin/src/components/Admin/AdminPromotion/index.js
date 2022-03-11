import { React, Fragment, useState, useEffect } from "react";
import AdminService from "../../../api/AdminService";
import ProductService from "../../../api/ProductService";
import { auth_action } from "../../../redux/auth_slice.js";
import { toast } from "react-toastify";
import AuthService from "../../../api/AuthService";
import Pagination from "../../../shared-components/Pagination";
import ProductTableItem from "../TableItem/Product";
import AdminHeader from "../AdminHeader";
import AdminFooter from "../AdminFooter";
import AddProductModal from "../../Modal/AddProduct";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import SearchModal from "../../Modal/SearchModal";
import GenreTableItem from "../TableItem/Genre";
import AddUserModal from "../../Modal/AddUser";
import AddGenreModal from "../../Modal/AddGenre";
import Loading from "../../../shared-components/Loading";

function AdminPromotion() {
  const [authorizing, setAuthorizing] = useState(true);
  const [isLoading, setIsLoading] = useState(false);
  const [reRender, setReRender] = useState(true);
  
  var navigate = useNavigate();
  const dispatch = useDispatch();
  const isLoggedIn = useSelector((state) => state.auth_slice.isLoggedIn);
  const user = useSelector((state) => state.auth_slice.user);

  useEffect(() => {
    AuthService.GetAuthorizeAdmin()
      .then((res) => {
        //console.log(res.data);
        setAuthorizing(false);
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


  const [status, setStatus] = useState("all");
  const [orderby, setOrderby] = useState("Id");
  const [sort, setSort] = useState("Asc");
  const [pageNumber, setpageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalPage, setTotalPage] = useState(1);

  const [listPromo, setListPromo] = useState([]);
  //function
  function onStatusFilterChange(event) {
    setpageNumber(1);
    setStatus(event.target.value);
  }
  function onOrderByFilterChange(event) {
    setpageNumber(1);
    setOrderby(event.target.value);
  }
  function onSortFilterChange(event) {
    setpageNumber(1);
    setSort(event.target.value);
  }
  function onPageSizeFilterChange(event) {
    setpageNumber(1);
    setPageSize(event.target.value);
  }
  function onPageNumberChange(event) {
    setpageNumber(event.target.value);
  }
  function onArrowPaginationClick(event) {
    var id = event.target.id.slice(0, 12);
    if (id == "pagination_r") {
      if (pageNumber < totalPage) {
        setpageNumber(pageNumber + 1);
      }
    } else {
      if (pageNumber > 1) {
        setpageNumber(pageNumber - 1);
      }
    }
  }
  function ReRender() {
    setReRender(!reRender);
  }

  const defaultImgUrl =
    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png";
  const [selectedImgUrl, setselectedImgUrl] = useState(null);
  function onImageChange(event) {
    //setselectedImgUrl(event.target.value)
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      alert("Bạn phải chọn 1 hình ảnh");
      return;
    }

    var mimeType = event.target.files[0].type;

    if (mimeType.match(/image\/*/) == null) {
      alert("File phải là hình ảnh");
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);

    reader.onload = (_event) => {
      // console.log(reader.result)
      setselectedImgUrl(reader.result);
    };
  }

  const [showAddModal, setShowAddModal] = useState(false);
  const [isAdding, setIsAdding] = useState(false);
  let {
    register: registerAddModal,
    handleSubmit: handleSubmitAddModal,
    watch,
    reset: resetAddModal,
    formState: { errors: addModalError },
  } = useForm();

  const handleCloseAddModal = () => {
    setShowAddModal(false);
    resetAddModal();
    setselectedImgUrl(null);
  };
  const handleShowAddModal = () => {
    setShowAddModal(true);
  };

  function onAddButtonClick(data) {
    var startDate = new Date(data.startDate);
    var endDate = new Date(data.endDate);
    if (startDate.getTime() >= endDate.getTime()) {
      toast.error("Ngày bắt đầu và ngày kết thúc chưa hợp lệ!", {
        position: "top-center",
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    } else {
      setIsAdding(true);
      console.log();
      var promo = {
        name: data.name,
        description: data.description,
        imgUrl: selectedImgUrl,
        startDate: startDate.toISOString(),
        endDate: endDate.toISOString(),
        status: 0,
        visible: 0,
      };
      AdminService.PostPromotion(promo)
        .then((response) => {
          console.log(response.data);
          if (response.data.success) {
            toast.success("Thêm thành công!", {
              position: "top-center",
              autoClose: 1000,
              hideProgressBar: true,
              closeOnClick: true,
              pauseOnHover: true,
              draggable: true,
            });
          } else {
            toast.error("Có lỗi xảy ra! Xin hãy thử lại", {
              position: "top-center",
              autoClose: 1000,
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
          setIsAdding(false);
          setShowAddModal(false);
          resetAddModal();
          ReRender();
        });
    }
  }
  //run first

  useEffect(() => {
    setIsLoading(true);
    AdminService.GetPromotionForAdmin(
      status,
      orderby,
      sort,
      pageNumber,
      pageSize
    )
      .then((response) => {
        //console.log(response.data);
        setListPromo(response.data.result);
        setTotalPage(Math.ceil(Number(response.data.total / pageSize)));
      })
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [status, orderby, sort, pageNumber, pageSize, reRender]);

  var today = new Date();
  var minDay = "";
  var maxDay = "";
  var dd = today.getDate();
  var mm = today.getMonth() + 1; //January is 0!
  var yyyy = today.getFullYear();
  if (dd < 10) {
    dd = "0" + dd;
  }

  if (mm < 10) {
    mm = "0" + mm;
  }
  minDay = yyyy + "-" + mm + "-" + dd;
  dd = today.getDate() + 1;
  maxDay = yyyy + "-" + mm + "-" + dd;

  return (
    <Fragment>
      {!authorizing && (
        <Fragment>
          <AdminHeader></AdminHeader>

          <div className="w-100 h-100" style={{ backgroundColor: "#1E1E28" }}>
            <div className="container  py-3 ">
              <div className="card p-3 mb-3">
                <p className="lead text-center mb-0 fw-bold fs-3 text-monospace">
                  {" "}
                  <i className="fas fa-file-invoice-dollar me-2"></i>Quản lý
                  khuyến mãi
                </p>
              </div>

              <div className="row">
                <hr className="text-white"></hr>
                <div className="d-flex flex-wrap justify-content-around ">
                  <div className="mb-3 row">
                    <label className="text-white">Trạng thái: </label>
                    <select
                      className="form-select"
                      defaultValue={"all"}
                      onChange={onStatusFilterChange}
                    >
                      <option value="all">Toàn bộ</option>
                      <option value="0">Chưa bắt đầu</option>
                      <option value="1">Đã bắt đầu</option>
                    </select>
                  </div>
                  <div className="mb-3 row">
                    <label className="text-white">Sắp xếp theo: </label>
                    <select
                      className="form-select"
                      defaultValue={"Id"}
                      onChange={onOrderByFilterChange}
                    >
                      <option value="Id">Id</option>
                      <option value="totalPrice">Tổng giá</option>
                      <option value="contactName">Tên</option>
                    </select>
                  </div>
                  <div className="mb-3 row">
                    <label className="text-white">Asc/Desc: </label>
                    <select
                      className="form-select"
                      defaultValue={"Asc"}
                      onChange={onSortFilterChange}
                    >
                      <option value="Asc">Asc</option>
                      <option value="Desc">Desc</option>
                    </select>
                  </div>
                  <div className="mb-3 row">
                    <label className="text-white">Hiển thị: </label>
                    <select
                      className="form-select"
                      defaultValue={"5"}
                      onChange={onPageSizeFilterChange}
                    >
                      <option value="5">5</option>
                      <option value="2">2</option>
                      <option value="10">10</option>
                      <option value="20">20</option>
                      <option value="50">50</option>
                    </select>
                  </div>
                </div>
                <hr className="text-white"></hr>
                <div className="container">
                  <div className="card bg-admin text-white">
                    <div className="card-header">
                      <div className="d-flex justify-content-between flex-wrap">
                        <div className="col-sm-12 ">
                          <h5 className="card-title">
                            Bảng quản lý khuyến mãi
                          </h5>
                        </div>
                        <div className="col-sm-12 ">
                          <div className="btn-group mb-2">
                            <button
                              type="button"
                              className="btn btn-danger"
                              onClick={handleShowAddModal}
                            >
                              <i className="fas fa-plus"></i>
                            </button>
                            <button
                              type="button"
                              className="btn btn-warning"
                              onClick={ReRender}
                            >
                              <i className="fas fa-sync"></i>
                            </button>
                            <button type="button" className="btn btn-success">
                              <i className="fas fa-download"></i>
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="card-body text-white">
                      <div className="table-responsive ">
                        <table className="table">
                          <thead className="text-primary">
                            <tr>
                              <th className="text-center">#</th>
                              <th>Thông tin khuyến mãi</th>
                              <th>Ngày bắt đầu</th>
                              <th>Ngày kết thúc</th>
                              <th>Số sản phẩm</th>
                              <th className="text-right">Actions</th>
                            </tr>
                          </thead>
                          {!isLoading && listPromo.length > 0 && (
                            <tbody>
                              {listPromo.map((item, i) => {
                                return (
                                  <PromotionTableItem
                                    item={item}
                                    key={i}
                                    reRender={ReRender}
                                  ></PromotionTableItem>
                                );
                              })}
                            </tbody>
                          )}
                        </table>
                        {!isLoading && listPromo.length == 0 && (
                          <div className="d-flex justify-content-center">
                            {/* <p className="text-center text-white">
                              Không có dữ liệu
                            </p> */}
                            <img
                              className="img-fluid"
                              alt="nodata"
                              src="https://ringxe.vn/static/imgs/nodata-found.png"
                            ></img>
                          </div>
                        )}
                        {isLoading && (
                          <div className="d-flex justify-content-center">
                            <div
                              className="spinner-border text-info"
                              role="status"
                            >
                              <span className="visually-hidden">
                                Loading...
                              </span>
                            </div>
                            <p className="text monospace ms-2">
                              Đang xử lý xin chờ tí...
                            </p>
                          </div>
                        )}
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div className="d-flex justify-content-center mt-3">
                <div className="paginationsa:container">
                  <div
                    className="paginationsa:number arrow"
                    id="pagination_l"
                    onClick={onArrowPaginationClick}
                  >
                    <i
                      className="fas fa-chevron-left"
                      id="pagination_l_icon"
                    ></i>
                  </div>
                  <Pagination
                    totalPage={totalPage}
                    onPageNumberChange={onPageNumberChange}
                    pageNumber={pageNumber}
                  ></Pagination>
                  <div
                    className="paginationsa:number arrow"
                    id="pagination_r"
                    onClick={onArrowPaginationClick}
                  >
                    <i
                      className="fas fa-chevron-right"
                      id="pagination_r_icon"
                    ></i>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <Footer></Footer>
        </Fragment>
      )}
      {/* add modal */}
      <Modal show={showAddModal} onHide={handleCloseAddModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Thêm chương trình khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên chương trình : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Tên..."
                  {...registerAddModal("name", {
                    required: true,
                  })}
                ></input>
              </div>
              {addModalError.name?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Tên chương
                  trình không để trống
                </p>
              )}
              <label>Mô tả : </label>
              <div className="input-group">
                <textarea
                  placeholder="Mô tả..."
                  className="form-control "
                  {...registerAddModal("description", {
                    required: true,
                  })}
                ></textarea>
              </div>
              {addModalError.description?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Mô tả sản phẩm
                  không để trống
                </p>
              )}
              <label>Ngày bắt đầu : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Ngày bắt đầu.."
                  type="date"
                  min={minDay}
                  {...registerAddModal("startDate", {
                    required: true,
                  })}
                ></input>
              </div>
              {addModalError.startDate?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Ngày bắt đầu
                  không để trống
                </p>
              )}
              <label>Ngày kết thúc : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Ngày bắt đầu.."
                  type="date"
                  min={maxDay}
                  {...registerAddModal("endDate", {
                    required: true,
                  })}
                ></input>
              </div>
              {addModalError.endDate?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Ngày kết thúc
                  không để trống
                </p>
              )}
              <label>Ảnh QC</label>
              <div className="input-group">
                <input
                  type="file"
                  className="form-control"
                  placeholder="Ảnh"
                  onChange={onImageChange}
                ></input>
              </div>
              <img
                className="admin_img_modal"
                alt="Ảnh sản phẩm"
                src={selectedImgUrl ? selectedImgUrl : defaultImgUrl}
              ></img>
            </div>
          </form>
        </Modal.Body>
        {isAdding && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xủ lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button className="btn btn-danger" onClick={handleCloseAddModal}>
            Close
          </button>
          <button
            disabled={isAdding}
            className="btn btn-success"
            onClick={handleSubmitAddModal(onAddButtonClick)}
          >
            Save
          </button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
}

export default AdminPromotion;
