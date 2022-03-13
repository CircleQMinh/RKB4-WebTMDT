import React, { Fragment, useState } from "react";
import Modal from "react-bootstrap/Modal";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import AdminService from "../../../api/AdminService";
import { upLoadImageToCloudinary } from "../../../helper/Cloudinary";

function AddPromotionModal(props) {
  var showAddModal = props.showAddModal;
  var setShowAddModal = props.setShowAddModal;
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
  function ReRender() {
    props.reRender();
  }
  const [uploadImg, setUploadImg] = useState(false);
  const [selectedImgUrl, setselectedImgUrl] = useState(null);
  const defaultImgUrl =
    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png";
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
      if (uploadImg) {
        upLoadImageToCloudinary(
          selectedImgUrl == null ? defaultImgUrl : selectedImgUrl
        )
          .then((res) => {
            setselectedImgUrl(res.data.url);
            AddPromotion(data, res.data.url);
          })
          .catch((e) => {
            console.log(e);
            setIsAdding(false);
            setShowAddModal(false);
          });
      } else {
        AddPromotion(data);
      }
    }

    function AddPromotion(data, url) {
      var promo = {
        name: data.name,
        description: data.description,
        imgUrl: url ? url : selectedImgUrl,
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

  return (
    <Fragment>
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

export default AddPromotionModal;
