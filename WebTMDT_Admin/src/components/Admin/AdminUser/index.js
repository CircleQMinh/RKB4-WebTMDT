import { React, Fragment, useState, useEffect } from "react";
import { auth_action } from "../../../redux/auth_slice.js";
import AdminService from "../../../api/AdminService";
import AuthService from "../../../api/AuthService";
import UserTableItem from "./../TableItem/User";
import Pagination from "../../../shared-components/Pagination"
import { toast } from "react-toastify";
import SearchModal from "./../../Modal/SearchModal";
import AdminHeader from "../AdminHeader";
import AdminFooter from "../AdminFooter";
import { useNavigate } from "react-router-dom";
import AddUserModal from "../../Modal/AddUser";
import { useSelector, useDispatch } from 'react-redux';
import Loading from "../../../shared-components/Loading.js";
function AdminUser() {
  const [authorizing, setAuthorizing] = useState(true);
  var navigate = useNavigate()
  const dispatch = useDispatch();
  const isLoggedIn = useSelector((state) => state.auth_slice.isLoggedIn);
  const user = useSelector((state) => state.auth_slice.user);
  const [isLoading, setIsLoading] = useState(false);
  const [reRender, setReRender] = useState(true);
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
      setTimeout(()=>{
        dispatch(auth_action.logOut());
        navigate("/login")
      },2500)
    })
    .finally(() => {});
  },[reRender])


  const [showAddModal, setShowAddModal] = useState(false);

  const handleShowAddModal = () => {
    setShowAddModal(true);
  };


  const [orderby, setOrderby] = useState("orderDate");
  const [sort, setSort] = useState("Desc");
  const [pageNumber, setpageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalPage, setTotalPage] = useState(1);

  const [listUser, setListUser] = useState([]);
  //function
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


  //run first

  useEffect(() => {
    setIsLoading(true);
    AdminService.GetUserForAdmin(orderby, sort, pageNumber, pageSize)
      .then((response) => {
        //console.log(response.data);
        setListUser(response.data.result);
        setTotalPage(Math.ceil(Number(response.data.total / pageSize)));
      })
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [orderby, sort, pageNumber, pageSize, reRender]);

  const [searchType, setSearchType] = useState("User");
  const [searchBy, setSearchBy] = useState("Name");
  const [keyword, setKeyword] = useState("");
  const [searchResult, setSearchResult] = useState([]);
  const [currentResultPage, setCurrentResultPage] = useState(1);
  const [totalResultPage, setTotalResultPage] = useState(1);
  function onSearchTypeChange(event) {
    setSearchType(event.target.value);
  }
  function onSearchByChange(event) {
    setSearchBy(event.target.value);
  }
  function onKeywordChange(event) {
    setKeyword(event.target.value);
  }

  const [showSearchModal, setShowSearchModal] = useState(false);
  const [isSearching, setIsSearching] = useState(true);

  const handleCloseSearchModal = () => {
    setShowSearchModal(false);
  };
  const handleShowSearchModal = () => {
    setShowSearchModal(true);
    setIsSearching(true);
    GetSearchResult(1, 4);
  };
  function GetSearchResult(pageNumber, pageSize) {
    setCurrentResultPage(pageNumber);
    AdminService.GetSearchResult_User(searchBy, keyword, pageNumber, pageSize)
      .then((res) => {
        //console.log(res.data);
        setTotalResultPage(Math.ceil(Number(res.data.total / pageSize)));
        setSearchResult(res.data.result);
      })
      .catch((e) => {
        console.log(e);
      })
      .finally(() => {
        setIsSearching(false);
      });
  }
  function handleKeyDown(event) {
    if (event.keyCode === 13) {
      handleShowSearchModal();
    }
  }
  return (
    <Fragment>
      {!authorizing && (
        <Fragment>
          <AdminHeader></AdminHeader>

          <div className="w-100 h-100" style={{ backgroundColor: "#1E1E28" }}>
            <div className="container  py-3 ">
              <div className="card p-3">
                <p className="lead text-center mb-0 fw-bold fs-3 text-monospace">
                  {" "}
                  <i className="fas fa-file-invoice-dollar me-2"></i>Quản lý
                  người dùng
                </p>
              </div>
              <div className="row">
                <div className="col">
                  <div
                    className="div-center-content mt-3"
                    style={{ marginTop: -25 + "px", marginBottom: 15 + "px" }}
                    id="searchBarProduct"
                  >
                    <div className="w-100 my-2">
                      <div className="search">
                        <input
                          type="text"
                          className="searchTerm"
                          placeholder="Tìm kiếm..."
                          onKeyDown={handleKeyDown}
                          onChange={onKeywordChange}
                        ></input>
                        <button type="submit" className="searchButton" onClick={handleShowSearchModal}>
                          <i className="fa fa-search"></i>
                        </button>
                      </div>
                    </div>
                    <div className="form-group">
                      <label className="text-white fw-bold fs-5">
                        Tìm kiếm bằng :{" "}
                      </label>
                      <select className="form-select" defaultValue={"Name"} onChange={onSearchByChange}>
                        <option value="Id">Id</option>
                        <option value="Name">Tên</option>
                      </select>
                    </div>
                  </div>
                </div>
              </div>
              <div className="row">
                <hr className="text-white"></hr>
                <div className="d-flex flex-wrap justify-content-around ">
                  <div className="mb-3 row">
                    <label className="text-white">Sắp xếp theo: </label>
                    <select
                      className="form-select"
                      defaultValue={"Id"}
                      onChange={onOrderByFilterChange}
                    >
                      <option value="Id">Id</option>
                      <option value="Name">Tên</option>
                      <option value="Email">Email</option>
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
                            Bảng quản lý người dùng
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
                              <th>Thông tin</th>
                              <th>Email</th>
                              <th>SDT</th>
                              <th>Đã xác thực</th>
                              <th className="text-right">Xu</th>
                              <th className="text-right">Actions</th>
                            </tr>
                          </thead>
                          {!isLoading && listUser.length > 0 && (
                            <tbody>
                              {listUser.map((item, i) => {
                                return (
                                  <UserTableItem
                                    item={item}
                                    key={i}
                                    reRender={ReRender}
                                  ></UserTableItem>
                                );
                              })}
                            </tbody>
                          )}
                        </table>
                        {!isLoading && listUser.length == 0 && (
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
          <AdminFooter></AdminFooter>
        </Fragment>
      )}
      {authorizing && (
     <Loading></Loading>
      )}
      <AddUserModal
        showAddModal={showAddModal}
        setShowAddModal={setShowAddModal}
        reRender={ReRender}
      ></AddUserModal>                 

      <SearchModal
        showSearchModal={showSearchModal}
        handleCloseSearchModal={handleCloseSearchModal}
        isSearching={isSearching}
        searchResult = {searchResult}
        searchType = {searchType}
        GetSearchResult = {GetSearchResult}
        currentResultPage = {currentResultPage}
        totalResultPage = {totalResultPage}
      ></SearchModal>


    </Fragment>
  );
}

export default AdminUser;