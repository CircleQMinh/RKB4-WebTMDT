import React, { Fragment, useState } from "react";
import Modal from "react-bootstrap/Modal";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import AdminService from "../../../api/AdminService";
import { upLoadImageToCloudinary } from "../../../helper/Cloudinary";

function AddUserModal(props) {
  var showAddModal = props.showAddModal;
  var setShowAddModal = props.setShowAddModal;
  function ReRender() {
    props.reRender();
  }

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
    setselectedImgUrl(null);
    resetAddModal();
  };

  const [selectedImgUrl, setselectedImgUrl] = useState(null);
  const [uploadImg, setUploadImg] = useState(false);

  const defaultImgUrl =
    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png";
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
      setUploadImg(true);
    };
  }

  function onAddButtonClick(data) {
    setIsAdding(true);
    if (uploadImg==true) {
      upLoadImageToCloudinary(selectedImgUrl==null?defaultImgUrl:selectedImgUrl)
        .then((res) => {
          setselectedImgUrl(res.data.url);
          Add_User(data, res.data.url);
        })
        .catch((e) => {
          console.log(e);
          setIsAdding(false);
          setShowAddModal(false);
        });
    } else {
      Add_User(data);
    }
  }

  function Add_User(data, url) {
    var newUser = {
      email: data.email,
      password: data.password,
      imgUrl: url ? url : selectedImgUrl,
      userName: data.userName,
      phoneNumber: data.phoneNumber,
      roles: ["User"],
    };
    //console.log(newUser);

    AdminService.AddUserForAdmin(newUser)
      .then((res) => {
        if (res.data.success) {
          toast.success("Thêm thành công!", {
            position: "top-center",
            autoClose: 1000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
        } else {
          toast.error(res.data.error, {
            position: "top-center",
            autoClose: 1000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
        }
      })
      .catch((e) => {
        console.log(e);
      })
      .finally(() => {
        setIsAdding(false);
        ReRender();
        resetAddModal();
        setselectedImgUrl(null);
        setShowAddModal(false);
      });
  }
  return (
    <Fragment>
      {" "}
      {/* add modal */}
      <Modal show={showAddModal} onHide={handleCloseAddModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Thêm người dùng </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên người dùng : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Tên..."
                  {...registerAddModal("userName", {
                    required: true,
                  })}
                ></input>
              </div>
              {addModalError.userName?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Tên người dùng
                  không để trống
                </p>
              )}
              <label>Password : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Password.."
                  type="password"
                  {...registerAddModal("password", {
                    required: true,
                  })}
                ></input>
              </div>
              {addModalError.password?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Password không
                  để trống
                </p>
              )}
              <label>Email : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Email.."
                  type="email"
                  {...registerAddModal("email", {
                    required: true,
                    pattern:
                      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                  })}
                ></input>
              </div>
              {addModalError.email?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Email không để
                  trống
                </p>
              )}
              {addModalError.email?.type === "pattern" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Email không hợp
                  lệ
                </p>
              )}
              <label>SDT : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="SDT.."
                  type="tel"
                  {...registerAddModal("phoneNumber", {
                    required: true,
                    minLength: 9,
                    maxLength: 10,
                  })}
                ></input>
              </div>
              {addModalError.phoneNumber?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>SDT không để
                  trống
                </p>
              )}
              {addModalError.phoneNumber?.type === "minLength" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>SDT không hợp
                  lệ
                </p>
              )}
              {addModalError.phoneNumber?.type === "maxLength" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>SDT không hợp
                  lệ
                </p>
              )}
              <label>Ảnh SP</label>
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

export default AddUserModal;
