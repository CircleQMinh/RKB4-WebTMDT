import React, { Fragment, useState } from "react";
import Modal from "react-bootstrap/Modal";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import AdminService from "../../../api/AdminService";
import { upLoadImageToCloudinary } from "../../../helper/Cloudinary";

function AddGenreModal(props) {
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
    resetAddModal();
  };

  function onAddButtonClick(data) {
    console.log(data);
    setIsAdding(true)
    var newGenre = {
      name: data.name,
      description: data.description,
    };

    AdminService.AddGenreForAdmin(newGenre)
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
        resetAddModal();
        setShowAddModal(false);
        ReRender();
      });
  }

  return (
    <Fragment>
      {" "}
      {/* add modal */}
      <Modal show={showAddModal} onHide={handleCloseAddModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Thêm thể loại </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên thể loại : </label>
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
                  <i className="fas fa-exclamation-triangle"></i>Tên thể loại
                  không để trống
                </p>
              )}
              <label>Mô tả : </label>
              <div className="input-group">
                <textarea
                  placeholder="Mô tả thể loại"
                  className="form-control "
                  {...registerAddModal("description", {
                    required: true,
                  })}
                ></textarea>
              </div>
              {addModalError.description?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Mô tả thể loại
                  không để trống
                </p>
              )}
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

export default AddGenreModal;
