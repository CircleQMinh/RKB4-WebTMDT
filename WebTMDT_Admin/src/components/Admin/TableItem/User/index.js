import { React, Fragment, useState } from "react";
import NumberFormat from "react-number-format";
import { toast } from "react-toastify";
import Modal from "react-bootstrap/Modal";

import { useForm } from "react-hook-form";
import AdminService from "../../../../api/AdminService";
function UserTableItem(props) {
  var item = props.item;
  //console.log(item);

  let {
    register: registerEditModal,
    handleSubmit: handleSubmitEditModal,
    watch,
    reset: resetEditModal,
    formState: { errors: editModalError },
  } = useForm();

  const [selectedImgUrl, setselectedImgUrl] = useState(null);
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
    };
  }
  const [showEditModal, setShowEditModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const handleCloseEditModal = () => {
    setShowEditModal(false);
    setselectedImgUrl(null);
  };
  const handleShowEditModal = () => {
    setShowEditModal(true);
    setselectedImgUrl(item.imgUrl);
  };
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const handleCloseDeleteModal = () => setShowDeleteModal(false);
  const handleShowDeleteModal = () => {
    setShowDeleteModal(true);
  };

  function onSave_EditModal(data) {
    console.log(data);
    var newUser = {
      imgUrl: selectedImgUrl ? selectedImgUrl : defaultImgUrl,
      userName: data.userName,
      phoneNumber: data.phoneNumber,
    };
    console.log(newUser);
    setIsEditing(true);
    AdminService.EditUserForAdmin(item.id, newUser)
      .then((res) => {
        if (res.data.success) {
          toast.success("Chỉnh sửa thành công!", {
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
      .catch((e) => {
        console.log(e);
      })
      .finally(() => {
        setIsEditing(false);
        setShowEditModal(false);
        resetEditModal();
        setselectedImgUrl(null);
        props.reRender();
      });
  }
  function onDelete_DeleteModal() {
    setIsDeleting(true);
    AdminService.DeleteUserForAdmin(item.id)
      .then((res) => {
        if (res.data.success) {
          toast.success("Xóa thành công!", {
            position: "top-center",
            autoClose: 1000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
        } else {
          toast.error("Không thể xóa người dùng! Xin hãy thử lại", {
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
        setIsDeleting(false);
        setShowDeleteModal(false);
        props.reRender();
      });
  }

  return (
    <Fragment>
      <tr>
        <td className="text-center text-white">
          <div className="photo d-flex justify-content-start align-items-center">
            <img
              className="admin_img_table me-2"
              src={item.imgUrl}
              alt="photoimg"
            ></img>
            <p>
              {item.userName} ({item.roles[0]})
            </p>
          </div>
        </td>
        <td className="text-white">{item.email}</td>
        <td className="text-white">{item.phoneNumber}</td>
        <td className="text-white">
          {item.emailConfirmed && <i className="fa-solid fa-check"></i>}
          {!item.emailConfirmed && <i className="fa-solid fa-xmark"></i>}
        </td>
        <td className="text-white">{item.coins}</td>
        <td className="text-white">
          <div className="btn-group">
            <button type="button" className="btn btn-warning">
              <i className="fas fa-info-circle"></i>
            </button>
            <button
              type="button"
              className="btn btn-primary"
              onClick={handleShowEditModal}
            >
              <i className="far fa-edit"></i>
            </button>
            {!item.roles.some((x) => x == "Administrator") && (
              <button
                type="button"
                className="btn btn-danger"
                onClick={handleShowDeleteModal}
              >
                <i className="far fa-trash-alt"></i>
              </button>
            )}
          </div>
        </td>
      </tr>

      {/* edit modal */}
      <Modal show={showEditModal} onHide={handleCloseEditModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa thông tin </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <label>Tên người dùng : </label>
            <div className="input-group">
              <input
                className="form-control"
                defaultValue={item.userName}
                placeholder="Tên..."
                {...registerEditModal("userName", {
                  required: true,
                })}
              ></input>
            </div>
            {editModalError.userName?.type === "required" && (
              <p className="text-start m-0">
                <i className="fas fa-exclamation-triangle"></i>Tên người dùng
                không để trống
              </p>
            )}
            <label>SDT : </label>
            <div className="input-group">
              <input
                className="form-control"
                type="tel"
                defaultValue={item.phoneNumber}
                {...registerEditModal("phoneNumber", {
                  required: true,
                  minLength: 9,
                  maxLength: 10,
                })}
              ></input>
            </div>
            {editModalError.phoneNumber?.type === "required" && (
              <p className="text-start m-0">
                <i className="fas fa-exclamation-triangle"></i>SDT không để
                trống
              </p>
            )}
            {editModalError.phoneNumber?.type === "minLength" && (
              <p className="text-start m-0">
                <i className="fas fa-exclamation-triangle"></i>SDT không hợp lệ
              </p>
            )}
            {editModalError.phoneNumber?.type === "maxLength" && (
              <p className="text-start m-0">
                <i className="fas fa-exclamation-triangle"></i>SDT không hợp lệ
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
          </form>
        </Modal.Body>
        {isEditing && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xủ lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button className="btn btn-danger" onClick={handleCloseEditModal}>
            Close
          </button>
          <button
            disabled={isEditing}
            className="btn btn-success"
            onClick={handleSubmitEditModal(onSave_EditModal)}
          >
            Save
          </button>
        </Modal.Footer>
      </Modal>
      {/* delete modal */}
      <Modal show={showDeleteModal} onHide={handleCloseDeleteModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Xóa thông tin </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p className="text-tron text-monospace">Xóa người dùng này?</p>
          <p className="text-center">
            <i className="fas fa-exclamation-triangle"></i>Bất cứ thông tin nào
            liên quan đến người dùng sẽ bị xóa!
          </p>
        </Modal.Body>
        {isDeleting && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xủ lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button className="btn btn-danger" onClick={handleCloseDeleteModal}>
            Close
          </button>
          <button
            disabled={isDeleting}
            className="btn btn-success"
            onClick={onDelete_DeleteModal}
          >
            OK
          </button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
}

export default UserTableItem;
