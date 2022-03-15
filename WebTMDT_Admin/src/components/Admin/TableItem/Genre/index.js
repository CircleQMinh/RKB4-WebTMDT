import { React, Fragment, useState } from "react";
import { toast } from "react-toastify";
import Modal from "react-bootstrap/Modal";
import { useForm } from "react-hook-form";
import AdminService from "../../../../api/AdminService";
function GenreTableItem(props) {
  var item = props.item;
  //console.log(item);
  let {
    register: registerEditModal,
    handleSubmit: handleSubmitEditModal,
    watch,
    reset: resetEditModal,
    formState: { errors: editModalError },
  } = useForm();
  const [showEditModal, setShowEditModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const handleCloseEditModal = () => {
    setShowEditModal(false);
  };
  const handleShowEditModal = () => {
    setShowEditModal(true);
  };
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const handleCloseDeleteModal = () => setShowDeleteModal(false);
  const handleShowDeleteModal = () => {
    setShowDeleteModal(true);
  };

  function onSave_EditModal(data) {
    var newGenre = {
      name: data.name,
      description: data.description,
    };
    setIsEditing(true)
    AdminService.EditGenreForAdmin(item.id,newGenre)
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
        setIsEditing(false);
        resetEditModal();
        setShowEditModal(false);
        props.reRender();
      });
  }
  function onDelete_DeleteModal() {
    setIsDeleting(true);
    AdminService.DeleteGenreForAdmin(item.id)
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
        setIsDeleting(false);
        setShowDeleteModal(false);
        props.reRender();
      });
  }

  return (
    <Fragment>
      <tr className="animate__animated animate__fadeIn">
        <td className="text-center text-white">{item.id}</td>
        <td className="text-white">{item.name}</td>
        <td className="text-white">{item.description}</td>
        <td className="text-white">{item.books.length}</td>
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

            <button
              type="button"
              className="btn btn-danger"
              onClick={handleShowDeleteModal}
            >
              <i className="far fa-trash-alt"></i>
            </button>
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
            <div className="form-group">
              <label>Tên thể loại : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Tên..."
                  defaultValue={item.name}
                  {...registerEditModal("name", {
                    required: true,
                  })}
                ></input>
              </div>
              {editModalError.name?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Tên thể loại
                  không để trống
                </p>
              )}
              <label>Mô tả : </label>
              <div className="input-group">
                <textarea
                  placeholder="Mô tả thể loại"
                  defaultValue={item.description}
                  className="form-control "
                  {...registerEditModal("description", {
                    required: true,
                  })}
                ></textarea>
              </div>
              {editModalError.description?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Mô tả thể loại
                  không để trống
                </p>
              )}
            </div>
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
          <p className="text-tron text-monospace">Xóa thể loại này?</p>
          <p className="text-center">
            <i className="fas fa-exclamation-triangle"></i>Bất cứ thông tin nào
            liên quan đến thể loại sẽ bị xóa!
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

export default GenreTableItem;
