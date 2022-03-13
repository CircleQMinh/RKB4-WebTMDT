import { React, Fragment, useState } from "react";
import NumberFormat from "react-number-format";
import { formatDate } from "../../../../helper/formatDate";
import Modal from "react-bootstrap/Modal";
import AdminService from "../../../../api/AdminService";

import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { upLoadImageToCloudinary } from "../../../../helper/Cloudinary";

function PromotionTableItem(props) {
  var item = props.item;

  //   console.log(orderDate.toLocaleString())
  //   console.log(item);

  const [promoInfosList, setPromoInfosList] = useState([]);

  const [showInfoModal, setShowInfoModal] = useState(false);
  const [isLoadingPromotionInfo, setIsLoadingPromotionInfo] = useState(false);

  const handleCloseInfoModal = () => setShowInfoModal(false);
  const handleShowInfoModal = () => {
    //setIsLoadingPromotionInfo(true);
    setShowInfoModal(true);
    AdminService.GetPromotionInfosForAdmin(item.id)
      .then((response) => {
        console.log(response.data);
        setPromoInfosList(response.data.result);
      })
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        setIsLoadingPromotionInfo(false);
      });
  };

  const defaultImgUrl =
  "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png";
const [selectedImgUrl, setselectedImgUrl] = useState(null);

const [uploadImg, setUploadImg] = useState(false);

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


  const [showEditModal, setShowEditModal] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  let {
    register: registerEditModal,
    handleSubmit: handleSubmitEditModal,
    watch,
    reset: resetEditModal,
    formState: { errors: EditModalError },
  } = useForm();

  const handleCloseEditModal = () => {
    setShowEditModal(false);
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
      setIsEditing(true);
      if (uploadImg) {
        upLoadImageToCloudinary(
          selectedImgUrl == null ? defaultImgUrl : selectedImgUrl
        )
          .then((res) => {
            setselectedImgUrl(res.data.url);
            Edit_Promotion(data, res.data.url);
          })
          .catch((e) => {
            console.log(e);
            setIsEditing(false);
            setShowEditModal(false);
          });
      } else {
        Edit_Promotion(data);
      }
    }
  }

  function Edit_Promotion(data,url){
    var startDate = new Date(data.startDate);
    var endDate = new Date(data.endDate);
    var promo = {
      name: data.name,
      description: data.description,
      imgUrl:  url ? url : selectedImgUrl,
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString(),
      status: Number(data.status),
      visible: Number(data.visible),
    };
    AdminService.PutPromotion(promo, item.id)
      .then((response) => {
        console.log(response.data);
        if (response.data.success) {
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
      .catch((error) => {
        console.log(error);
      })
      .finally(() => {
        setIsEditing(false);
        setShowEditModal(false);
        resetEditModal();
        props.reRender();
      });
  }

  function onDelete_DeleteModal() {
    setIsDeleting(true);
    AdminService.DeletePromotion(item.id)
      .then((response) => {
        if (response.data.success) {
          toast.success("Xóa thành công!", {
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
        toast.error("Có lỗi xảy ra! Xin hãy thử lại", {
          position: "top-center",
          autoClose: 1000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
        });
      })
      .finally(() => {
        setIsDeleting(false);
        setShowDeleteModal(false);
        props.reRender();
      });
  }

  const [showAddPromoInfoModal, setShowAddPromInfoModal] = useState(false);
  const [isLoadingPromoAbleProduct, setIsLoadingPromoAbleProduct] =
    useState(false);
  const [isAddingPromoInfo, setIsAddingPromoInfo] = useState(false);
  const [promotableProducts, setPromotableProducts] = useState([]);
  const [selectPromoProduct, setSelectPromoProduct] = useState({ title: null });

  const handleCloseAddPromInfoModal = () => setShowAddPromInfoModal(false);
  const handleShowAddPromInfoModal = () => {
    setShowAddPromInfoModal(true);
    setIsLoadingPromoAbleProduct(true);
    AdminService.GetPromotableProduct()
      .then((res) => {
        //console.log(res.data.result);
        setPromotableProducts(res.data.result);
      })
      .catch((e) => {
        console.log(e);
      })
      .finally(() => {
        setIsLoadingPromoAbleProduct(false);
      });
  };

  function onselect_SelectPromoProduct(product) {
    setSelectPromoProduct(product);
  }

  function onAdd_PromoProduct() {
    var valid = document.getElementById("add_promotable_product_value").validity
      .valid;
    var value = document.getElementById("add_promotable_product_value").value;
    var type = document.getElementById("add_promotable_product_type").value;
    var promoInfo = {
      bookId: selectPromoProduct.id,
      promotionPercent: null,
      promotionAmount: null,
    };
    if(selectPromoProduct==null){
        toast.error("Chưa chọn sản phẩm!", {
            position: "top-center",
            autoClose: 2000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
        return;
    }
    if (valid) {
      if (type == "percent") {
        if (value < 0 || value > 100) {
            toast.error("Giá trị nhập không hợp lệ", {
                position: "top-center",
                autoClose: 2000,
                hideProgressBar: true,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
              });
          return;
        }
        promoInfo.promotionPercent = value;
      }
      if (type == "amount") {
        if (value > selectPromoProduct.price) {
            toast.error("Giá trị nhập không hợp lệ", {
                position: "top-center",
                autoClose: 2000,
                hideProgressBar: true,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
              });
          return;
        }
        promoInfo.promotionAmount = value;
      }
      setIsAddingPromoInfo(true);
      AdminService.PostPromotionInfo(promoInfo, item.id)
        .then((response) => {
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
        .catch((e) => {
          console.log(e);
        })
        .finally(() => {
          setIsAddingPromoInfo(false);
          props.reRender();
        });
    }
  }

  const [showEditPromoInfoModal, setShowEditPromInfoModal] = useState(false);
  const [isEditingPromoInfo, setIsEditingPromoInfo] = useState(false);
  const handleCloseEditPromInfoModal = () => setShowEditPromInfoModal(false);
  const handleShowEditPromInfoModal = (promoInfo) => {
    setShowEditPromInfoModal(true);
    setSelectPromoProduct(promoInfo);
  };
  function onEdit_PromoProduct() {
    var valid = document.getElementById("add_promotable_product_value").validity.valid;
    var value = document.getElementById("add_promotable_product_value").value;
    var type = document.getElementById("add_promotable_product_type").value;
    var promoInfo = {
      promotionPercent: null,
      promotionAmount: null,
    };
    if (valid) {
      if (type == "percent") {
        if (value < 1 || value > 99) {
          toast.error("Giá trị nhập không hợp lệ", {
            position: "top-center",
            autoClose: 2000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
          return;
        }
        promoInfo.promotionPercent = value;
      }
      if (type == "amount") {
        if (value > selectPromoProduct.book.price) {
          toast.error("Giá trị nhập không hợp lệ", {
            position: "top-center",
            autoClose: 2000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
          });
          return;
        }
        promoInfo.promotionAmount = value;
      }
      setIsEditingPromoInfo(true);
      AdminService.PutPromotionInfo(promoInfo, selectPromoProduct.id)
        .then((response) => {
          if (response.data.success) {
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
          setIsEditingPromoInfo(false);
          props.reRender();
        });
    } else {
      toast.error("Giá trị nhập không hợp lệ", {
        position: "top-center",
        autoClose: 2000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }
  }

  const [showDeletePromoInfoModal, setShowDeletePromInfoModal] =
    useState(false);
  const [isDeletingPromoInfo, setIsDeletingPromoInfo] = useState(false);
  const handleCloseDeletePromInfoModal = () =>
    setShowDeletePromInfoModal(false);
  const handleShowDeletePromInfoModal = (promoInfo) => {
    setShowDeletePromInfoModal(true);
    setSelectPromoProduct(promoInfo);
  };
  function onDelete_PromoProduct() {
    console.log(selectPromoProduct.id);
    setIsDeletingPromoInfo(true);
    AdminService.DeletePromotionInfo(selectPromoProduct.id)
      .then((response) => {
        if (response.data.success) {
          toast.success("Xóa thành công!", {
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
        setIsDeletingPromoInfo(false);
        props.reRender();
      });
  }

  return (
    <Fragment>
      <tr>
        <td className="text-center text-white">{item.id}</td>
        <td className="text-white">
          <div className="d-flex flex-column-reverse align-items-center">
            <img
              className="img_promo_admin img-fluid me-2 "
              src={item.imgUrl}
              alt="photoimg"
            ></img>
            <p>{item.name}</p>
          </div>
        </td>
        <td className="text-white">
          {formatDate(new Date(item.startDate + "Z"), "dd-MM-yyyy HH:mm:ss")}
        </td>
        <td className="text-white">
          {formatDate(new Date(item.endDate + "Z"), "dd-MM-yyyy HH:mm:ss")}
        </td>
        <td className="text-white">{item.promotionInfos.length}</td>
        <td className="text-white">
          <div className="btn-group">
            <button
              type="button"
              className="btn btn-warning"
              onClick={handleShowInfoModal}
            >
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

      {/* details modal */}
      <Modal show={showInfoModal} onHide={handleCloseInfoModal} size="xl">
        <Modal.Header closeButton>
          <Modal.Title>Thông tin chương trình khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <button
            className="btn btn-danger"
            onClick={handleShowAddPromInfoModal}
          >
            +
          </button>
          <hr></hr>
          {isLoadingPromotionInfo && (
            <div className="d-flex flex-column align-items-center">
              <div className="spinner-border text-info" role="status">
                <span className="visually-hidden">Loading...</span>
              </div>

              <p className="text-center text-monospace mt-2">
                Đang xử lý xin chờ 1 xíu ...
              </p>
            </div>
          )}
          {!isLoadingPromotionInfo && (
            <div className="table-responsive ">
              <p>Tên chương trình : {item.name}</p>
              <p>Mô tả : {item.description}</p>

              <p>Bắt đầu : {formatDate(new Date(item.startDate),"dd-MM-yyyy")} </p>
              <p>Kết thúc : {formatDate(new Date(item.endDate),"dd-MM-yyyy")}</p>
              <table className="table text-white">
                <thead>
                  <tr>
                    <th>SP</th>
                    <th>Tên</th>
                    <th>Giá</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {promoInfosList.length > 0 &&
                    promoInfosList.map((od) => {
                      return (
                        <tr key={od.id}>
                          <td>
                            <img
                              className="admin_db_button"
                              src={od.book.imgUrl}
                              alt="productimg"
                            ></img>
                          </td>
                          <td>{od.book.title}</td>
                          {od.promotionAmount == null &&
                            od.promotionPercent == null && (
                              <td>
                                <NumberFormat
                                  value={od.book.price}
                                  className="text-center text-danger text-decoration-underline  "
                                  displayType={"text"}
                                  thousandSeparator={true}
                                  suffix={"đ"}
                                  renderText={(value, props) => (
                                    <span {...props}>{value}</span>
                                  )}
                                />
                              </td>
                            )}
                          {od.promotionAmount != null && (
                            <td>
                              <NumberFormat
                                value={od.book.price}
                                className="text-center text-danger text-decoration-line-through me-2"
                                displayType={"text"}
                                thousandSeparator={true}
                                suffix={"đ"}
                                renderText={(value, props) => (
                                  <span {...props}>{value}</span>
                                )}
                              />
                              <NumberFormat
                                value={od.book.price - od.promotionAmount}
                                className="text-center text-danger text-decoration-underline  "
                                displayType={"text"}
                                thousandSeparator={true}
                                suffix={"đ"}
                                renderText={(value, props) => (
                                  <span {...props}>{value}</span>
                                )}
                              />
                              <span className="badge rounded-pill bg-danger ms-3">
                                {`- ${od.promotionAmount} đ`}
                              </span>
                            </td>
                          )}
                          {od.promotionPercent != null && (
                            <td>
                              <NumberFormat
                                value={od.book.price}
                                className="text-center text-danger text-decoration-line-through me-2"
                                displayType={"text"}
                                thousandSeparator={true}
                                suffix={"đ"}
                                renderText={(value, props) => (
                                  <span {...props}>{value}</span>
                                )}
                              />
                              <NumberFormat
                                value={
                                  od.book.price -
                                  (od.book.price * od.promotionPercent) / 100
                                }
                                className="text-center text-danger text-decoration-underline "
                                displayType={"text"}
                                thousandSeparator={true}
                                suffix={"đ"}
                                renderText={(value, props) => (
                                  <span {...props}>{value}</span>
                                )}
                              />
                              <span className="badge rounded-pill bg-success ms-3">
                                -{od.promotionPercent}%
                              </span>
                            </td>
                          )}
                          <td>
                            <div className="btn-group">
                              <button
                                type="button"
                                className="btn btn-primary"
                                onClick={() => {
                                  handleShowEditPromInfoModal(od);
                                }}
                              >
                                <i className="far fa-edit"></i>
                              </button>
                              <button
                                type="button"
                                className="btn btn-danger"
                                onClick={() => {
                                  handleShowDeletePromInfoModal(od);
                                }}
                              >
                                <i className="far fa-trash-alt"></i>
                              </button>
                            </div>
                          </td>
                        </tr>
                      );
                    })}
                </tbody>
              </table>
            </div>
          )}
        </Modal.Body>
      </Modal>
      {/* edit modal */}
      <Modal show={showEditModal} onHide={handleCloseEditModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa chương trình khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên chương trình : </label>
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
              {EditModalError.name?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Tên chương
                  trình không để trống
                </p>
              )}
              <label>Mô tả : </label>
              <div className="input-group">
                <textarea
                  placeholder="Mô tả..."
                  defaultValue={item.description}
                  className="form-control "
                  {...registerEditModal("description", {
                    required: true,
                  })}
                ></textarea>
              </div>
              {EditModalError.description?.type === "required" && (
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
                  defaultValue={formatDate(
                    new Date(item.startDate + "Z"),
                    "yyyy-MM-dd"
                  )}
                  {...registerEditModal("startDate", {
                    required: true,
                  })}
                ></input>
              </div>
              {EditModalError.startDate?.type === "required" && (
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
                  defaultValue={formatDate(
                    new Date(item.endDate + "Z"),
                    "yyyy-MM-dd"
                  )}
                  {...registerEditModal("endDate", {
                    required: true,
                  })}
                ></input>
              </div>
              {EditModalError.endDate?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Ngày kết thúc
                  không để trống
                </p>
              )}
              <label>Trạng thái chương trình</label>
              <select
                className="form-select"
                defaultValue={item.status}
                {...registerEditModal("status", {
                  required: true,
                })}
              >
                <option value="0">Chưa bắt đầu</option>
                <option value="1">Đã bắt đầu</option>
              </select>
              <label>Hiển thị</label>
              <select
                className="form-select"
                defaultValue={item.visible}
                {...registerEditModal("visible", {
                  required: true,
                })}
              >
                <option value="0">Chưa hiển thị</option>
                <option value="1">Hiển thị</option>
              </select>
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
          <Modal.Title>Xóa chương trình </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p className="text-tron text-monospace">Xóa chương trình này?</p>
          <p className="text-center">
            <i className="fas fa-exclamation-triangle"></i>Bất cứ thông tin nào
            liên quan đến chương trình sẽ bị xóa!
          </p>
        </Modal.Body>
        {isDeleting && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xử lý xin chờ tí...</p>
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
      {/* hiển các sp chưa có khuyến mãi - thêm promoinfo*/}
      <Modal
        show={showAddPromoInfoModal}
        onHide={handleCloseAddPromInfoModal}
        size="lg"
      >
        <Modal.Header closeButton>
          <Modal.Title>Thêm sản phẩm khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body className="tbody_add_promo">
          {isLoadingPromoAbleProduct && (
            <div className="d-flex flex-column align-items-center">
              <div className="spinner-border text-info" role="status">
                <span className="visually-hidden">Loading...</span>
              </div>

              <p className="text-center text-monospace mt-2">
                Đang xử lý xin chờ 1 xíu ...
              </p>
            </div>
          )}
          {!isLoadingPromoAbleProduct && (
            <Fragment>
              <form>
                <label>Loại hình giảm giá</label>
                <select
                  className="form-select"
                  defaultValue="percent"
                  id="add_promotable_product_type"
                >
                  <option value="percent">%</option>
                  <option value="amount">VND</option>
                </select>
                <label>Giá trị giảm giá : </label>
                <div className="input-group">
                  <input
                    className="form-control"
                    placeholder="Giá trị giảm giá..."
                    id="add_promotable_product_value"
                    type="text"
                    pattern="[0-9]*"
                  ></input>
                </div>
              </form>
              <div className="table-responsive ">
                <label>Chọn sản phẩm : </label>
                <div className="input-group">
                  <input
                    className="form-control"
                    placeholder="(chọn từ danh sách)..."
                    list="products"
                    id="add_promotable_product_pick_product"
                    value={selectPromoProduct.title}
                    readOnly
                  ></input>
                  <datalist id="products">
                    {promotableProducts.map((p) => {
                      return (
                        <option value={p.title} key={p.id}>
                          {p.title}
                        </option>
                      );
                    })}
                  </datalist>
                </div>
              </div>
              <div className="select_product">
                <div className="table-responsive">
                  <table className="table text-white">
                    <thead>
                      <tr>
                        <th>Sản phẩm</th>
                        <th>Giá</th>
                        <th className="text-right">Actions</th>
                      </tr>
                    </thead>
                    <tbody className="">
                      {promotableProducts.length > 0 &&
                        promotableProducts.map((p) => {
                          return (
                            <tr key={p.id}>
                              <td>
                                <img
                                  alt="no"
                                  src={p.imgUrl}
                                  className="admin_img_table"
                                ></img>
                                <p className="d-inline">{p.title}</p>
                              </td>
                              <td>
                                {" "}
                                <NumberFormat
                                  value={p.price}
                                  className="text-center text-danger text-decoration-underline  "
                                  displayType={"text"}
                                  thousandSeparator={true}
                                  suffix={"đ"}
                                  renderText={(value, props) => (
                                    <p {...props}>{value}</p>
                                  )}
                                />
                              </td>
                              <td>
                                <button
                                  className="btn btn-sm btn-success"
                                  onClick={() => {
                                    onselect_SelectPromoProduct(p);
                                  }}
                                >
                                  Chọn
                                </button>
                              </td>
                            </tr>
                          );
                        })}
                    </tbody>
                  </table>
                </div>
              </div>
            </Fragment>
          )}
        </Modal.Body>
        {isAddingPromoInfo && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xử lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button
            className="btn btn-danger"
            onClick={handleCloseAddPromInfoModal}
          >
            Close
          </button>
          <button
            disabled={isAddingPromoInfo}
            className="btn btn-success"
            onClick={onAdd_PromoProduct}
          >
            OK
          </button>
        </Modal.Footer>
      </Modal>
      {/* chỉnh sửa các sp - sửa promoinfo*/}
      <Modal
        show={showEditPromoInfoModal}
        onHide={handleCloseEditPromInfoModal}
        size="lg"
      >
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa sản phẩm khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <label>Loại hình giảm giá</label>
            <select
              className="form-select"
              defaultValue={
                selectPromoProduct.promotionAmount ? "amount" : "percent"
              }
              id="add_promotable_product_type"
            >
              <option value="percent">%</option>
              <option value="amount">VND</option>
            </select>
            <label>Giá trị giảm giá : </label>
            <div className="input-group">
              <input
                className="form-control"
                placeholder="Giá trị giảm giá..."
                id="add_promotable_product_value"
                type="text"
                pattern="[0-9]*"
                defaultValue={
                  selectPromoProduct.promotionAmount
                    ? selectPromoProduct.promotionAmount
                    : selectPromoProduct.promotionPercent
                }
              ></input>
            </div>
            <label>Chọn sản phẩm : </label>
            <div className="input-group">
              <input
                className="form-control"
                id="add_promotable_product_pick_product"
                value={
                  selectPromoProduct.book
                    ? selectPromoProduct.book.title
                    : undefined
                }
                readOnly
              ></input>
            </div>
          </form>
        </Modal.Body>
        {isEditingPromoInfo && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xử lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button
            className="btn btn-danger"
            onClick={handleCloseEditPromInfoModal}
          >
            Close
          </button>
          <button
            disabled={isEditingPromoInfo}
            className="btn btn-success"
            onClick={onEdit_PromoProduct}
          >
            OK
          </button>
        </Modal.Footer>
      </Modal>
      {/* delete promoinfo modal - xóa promoinfo*/}
      <Modal
        show={showDeletePromoInfoModal}
        onHide={handleCloseDeletePromInfoModal}
        size="lg"
      >
        <Modal.Header closeButton>
          <Modal.Title>Xóa thông tin khuyến mãi </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p className="text-tron text-monospace">
            Xóa thông tin khuyến mãi này?
          </p>
          <p className="text-center">
            <i className="fas fa-exclamation-triangle"></i>Bất cứ thông tin nào
            liên quan đến thông tin khuyến mãi sẽ bị xóa!
          </p>
        </Modal.Body>
        {isDeletingPromoInfo && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2">Đang xử lý xin chờ tí...</p>
          </div>
        )}
        <Modal.Footer>
          <button
            className="btn btn-danger"
            onClick={handleCloseDeletePromInfoModal}
          >
            Close
          </button>
          <button
            disabled={isDeletingPromoInfo}
            className="btn btn-success"
            onClick={onDelete_PromoProduct}
          >
            OK
          </button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
}

export default PromotionTableItem;
