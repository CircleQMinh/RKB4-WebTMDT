import { React, Fragment, useState } from "react";
import NumberFormat from "react-number-format";
import { formatDate } from "../../../../helper/formatDate";
import Modal from "react-bootstrap/Modal";
import AdminService from "../../../../api/AdminService";

import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { upLoadImageToCloudinary } from "../../../../helper/Cloudinary";
function ProductTableItem(props) {
  var item = props.item;
  //console.log(item);
  const listGenre = props.listGenre;
  const listAuthor = props.listAuthor;
  const listPublisher = props.listPublisher;

  const defaultImgUrl =
    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png";
  const [selectedImgUrl, setselectedImgUrl] = useState(null);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [selectedAuthors, setSelectedAuthors] = useState([]);
  const [selectedPublisher, setSelectedPublisher] = useState();

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
    setselectedImgUrl(null);
    resetEditModal();
    setSelectedGenres([]);
    setSelectedAuthors([]);
    setSelectedPublisher(null);
  };
  const handleShowEditModal = () => {
    setShowEditModal(true);
    var g = [];
    item.genres.forEach((genre) => {
      g.push(genre.name);
    });
    setSelectedGenres(g);

    var a = [];
    item.authors.forEach((author) => {
      a.push(author.name);
    });
    setSelectedAuthors(a);

    setSelectedPublisher(item.publisher.name);
    setselectedImgUrl(item.imgUrl);
  };
  //modal Edit genre
  const [showEdit_EditGenreModal, setShowEdit_EditGenreModal] = useState(false);

  const handleCloseEdit_EditGenreModal = () => {
    setShowEdit_EditGenreModal(false);
  };
  const handleShowEdit_EditGenreModal = () => {
    setShowEdit_EditGenreModal(true);
  };

  function onEditGenreToProduct(event) {
    var genre = document.getElementById("pick_genre").value;
    var state = [...selectedGenres];
    if (!state.includes(genre)) {
      state.push(genre);
    }

    setSelectedGenres(state);
    setShowEdit_EditGenreModal(false);
  }
  function onRemoveGenreFromProduct(event) {
    var genre = event.target.outerText;
    var state = [...selectedGenres];
    setSelectedGenres(state.filter((q) => q != genre.trim()));
  }
  //modal Edit author
  const [showEdit_EditAuthorModal, setShowEdit_EditAuthorModal] =
    useState(false);

  const handleCloseEdit_EditAuthorModal = () => {
    setShowEdit_EditAuthorModal(false);
  };
  const handleShowEdit_EditAuthorModal = () => {
    setShowEdit_EditAuthorModal(true);
  };

  function onEditAuthorToProduct(event) {
    var au = document.getElementById("pick_author").value;
    var state = [...selectedAuthors];
    if (!state.includes(au)) {
      state.push(au);
    }

    setSelectedAuthors(state);
    setShowEdit_EditAuthorModal(false);
  }
  function onRemoveAuthorFromProduct(event) {
    var au = event.target.outerText;
    var state = [...selectedAuthors];
    setSelectedAuthors(state.filter((q) => q != au.trim()));
  }
  function onPublisherChange(event) {
    var publisher = event.target.value;
    setSelectedPublisher(publisher);
    // console.log(publisher)
  }

  function onEditButtonClick(data) {
    if (selectedGenres.length == 0) {
      toast.info("Thêm thể loại cho sản phẩm!", {
        position: "top-center",
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
      return;
    }
    if (selectedAuthors.length == 0) {
      toast.info("Thêm tác giả cho sản phẩm!", {
        position: "top-center",
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
      return;
    }
    if (selectedPublisher == null) {
      toast.info("Thêm nhà xuất bản cho sản phẩm!", {
        position: "top-center",
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
      return;
    }
    setIsEditing(true);
    if (uploadImg) {
      upLoadImageToCloudinary(selectedImgUrl)
        .then((res) => {
          setselectedImgUrl(res.data.url);
          EditProduct(data, res.data.url);
        })
        .catch((e) => {
          console.log(e);
          setIsEditing(false);
          setShowEditModal(false);
        });
    } else {
      EditProduct(data);
    }
  }

  function EditProduct(data, url) {
    var book = {
      title: data.title,
      description: data.description,
      imgUrl: url ? url : selectedImgUrl,
      price: data.price,
      publishYear: data.publishYear,
      numberOfPage: data.numberOfPage,
      genres: selectedGenres,
      authors: selectedAuthors,
      publisherName: selectedPublisher,
    };
    //console.log(book);

    AdminService.EditProduct(item.id, book)
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
        props.reRender();
      });
  }

  const [showDeleteProductModal, setShowDeleteProductModal] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);
  const handleCloseDeleteProductModal = () => setShowDeleteProductModal(false);
  const handleShowDeleteProductModal = () => {
    setShowDeleteProductModal(true);
  };

  function onDelete_DeleteProductModal() {
    setIsDeleting(true);
    AdminService.DeleteProduct(item.id)
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
        setShowDeleteProductModal(false);
        props.reRender();
      });
  }
  // console.log(item)
  return (
    <Fragment>
      <tr className="animate__animated animate__fadeIn">
        <td className="text-center text-white">{item.id}</td>
        <td className="text-white">
          <div className="d-flex flex-column justify-content-start align-items-center">
            <img
              className="admin_img_table mb-3"
              src={item.imgUrl}
              alt="photoimg"
            ></img>
            <p>{item.title}</p>
          </div>
        </td>
        <td className="text-white ">
          <div className="d-flex flex-column justify-content-between align-items-center">
          {item.promotionInfo == null && (
            <NumberFormat
              value={item.price}
              className="text-center text-danger text-decoration-underline my-2 "
              displayType={"text"}
              thousandSeparator={true}
              suffix={"đ"}
              renderText={(value, props) => <span {...props}>{value}</span>}
            />
          )}
          {item.promotionInfo != null &&
            item.promotionInfo.promotionAmount != null && (
              <Fragment>
                <NumberFormat
                  value={item.price}
                  className="text-center text-danger text-decoration-line-through "
                  displayType={"text"}
                  thousandSeparator={true}
                  suffix={"đ"}
                  renderText={(value, props) => <span {...props}>{value}</span>}
                />
                <NumberFormat
                  value={item.price - item.promotionInfo.promotionAmount}
                  className="text-center text-danger text-decoration-underline  my-4 "
                  displayType={"text"}
                  thousandSeparator={true}
                  suffix={"đ"}
                  renderText={(value, props) => <span {...props}>{value}</span>}
                />
                <span className="badge rounded-pill bg-danger ">
                  {`- ${item.promotionInfo.promotionAmount} đ`}
                </span>
              </Fragment>
            )}
          {item.promotionInfo != null &&
            item.promotionInfo.promotionPercent != null && (
              <Fragment>
                <NumberFormat
                  value={item.price}
                  className="text-center text-danger text-decoration-line-through "
                  displayType={"text"}
                  thousandSeparator={true}
                  suffix={"đ"}
                  renderText={(value, props) => <span {...props}>{value}</span>}
                />
                <NumberFormat
                  value={
                    item.price -
                    (item.price * item.promotionInfo.promotionPercent) / 100
                  }
                  className="text-center text-danger text-decoration-underline my-4 "
                  displayType={"text"}
                  thousandSeparator={true}
                  suffix={"đ"}
                  renderText={(value, props) => <span {...props}>{value}</span>}
                />
                <span className="badge rounded-pill bg-success ">
                  -{item.promotionInfo.promotionPercent}%
                </span>
              </Fragment>
            )}
          </div>

        </td>
        <td className="text-white">
          <p className="text-center">
            <i className="fa-solid fa-print me-2"></i> {item.publisher.name}{" "}
          </p>{" "}
          {item.authors.map((author) => {
            return (
              <p className="text-center" key={author.id}>
                {" "}
                <i className="fa-solid fa-user-pen me-2"></i>
                {author.name}
              </p>
            );
          })}
        </td>

        <td className="text-white">
          <p className="text-center"><i className="fa-solid fa-calendar-plus me-2"></i> {formatDate(new Date(item.createDate),"dd-MM-yyyy HH:mm:ss")} </p>
          <p className="text-center"><i className="fa-solid fa-calendar-check me-2"></i> {formatDate(new Date(item.updateDate),"dd-MM-yyyy HH:mm:ss")} </p>
        </td>
        <td className="text-white">
          {item.genres.map((genre) => {
            return (
              <p className="text-center" key={genre.id}>
                {" "}
                <i className="fa-solid fa-tag me-2"></i> {genre.name}
              </p>
            );
          })}
        </td>
        <td className="text-white">
          <div className="btn-group">
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
              onClick={handleShowDeleteProductModal}
            >
              <i className="far fa-trash-alt"></i>
            </button>
          </div>
        </td>
      </tr>

      {/* Edit  modal */}
      <Modal show={showEditModal} onHide={handleCloseEditModal} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa sản phẩm </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên sản phẩm : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Tên sản phẩm..."
                  defaultValue={item.title}
                  {...registerEditModal("title", {
                    required: true,
                  })}
                ></input>
              </div>
              {EditModalError.title?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Tên sản phẩm
                  không để trống
                </p>
              )}
              <label>Mô tả : </label>
              <div className="input-group">
                <textarea
                  placeholder="Mô tả sản phẩm"
                  className="form-control "
                  defaultValue={item.description}
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
              <label>Giá : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Giá sản phẩm.."
                  type="number"
                  defaultValue={item.price}
                  {...registerEditModal("price", {
                    required: true,
                    min: 1000,
                  })}
                ></input>
              </div>
              {EditModalError.price?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Giá sản phẩm
                  không để trống
                </p>
              )}
              {EditModalError.price?.type === "min" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Giá sản phẩm
                  không hợp lệ
                </p>
              )}
              <label>Năm xuất bản : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Năm xuất bản.."
                  defaultValue={item.publishYear}
                  type="number"
                  {...registerEditModal("publishYear", {
                    required: true,
                    min: 2000,
                    max: new Date().getFullYear(),
                  })}
                ></input>
              </div>
              {EditModalError.publishYear?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Năm xuất bản
                  không để trống
                </p>
              )}
              {(EditModalError.publishYear?.type === "min" ||
                EditModalError.publishYear?.type === "max") && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Năm xuất bản
                  không hợp lệ
                </p>
              )}
              <label>Số trang : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="Số trang.."
                  type="number"
                  defaultValue={item.numberOfPage}
                  {...registerEditModal("numberOfPage", {
                    required: true,
                    min: 1,
                  })}
                ></input>
              </div>
              {EditModalError.numberOfPage?.type === "required" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Số trang không
                  để trống
                </p>
              )}
              {EditModalError.numberOfPage?.type === "min" && (
                <p className="text-start m-0">
                  <i className="fas fa-exclamation-triangle"></i>Số trang không
                  hợp lệ
                </p>
              )}
              <label>Thể loại : </label>
              <div className="input-group">
                {selectedGenres.map((genre) => {
                  return (
                    <span
                      className="badge rounded-pill bg-warning text-dark mx-2  "
                      key={genre}
                      onClick={onRemoveGenreFromProduct}
                    >
                      {genre} <i className="far fa-times-circle"></i>
                    </span>
                  );
                })}

                <span
                  className="badge rounded-pill bg-info text-dark mx-2  "
                  onClick={handleShowEdit_EditGenreModal}
                >
                  +
                </span>
              </div>

              <label>Tác giả : </label>
              <div className="input-group">
                {selectedAuthors.map((a) => {
                  return (
                    <span
                      className="badge rounded-pill bg-warning text-dark mx-2  "
                      key={a}
                      onClick={onRemoveAuthorFromProduct}
                    >
                      {a} <i className="far fa-times-circle"></i>
                    </span>
                  );
                })}

                <span
                  className="badge rounded-pill bg-info text-dark mx-2  "
                  onClick={handleShowEdit_EditAuthorModal}
                >
                  +
                </span>
              </div>
              <label>Nhà xuất bản : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="(chọn từ danh sách hoặc thêm mới)..."
                  list="publishers"
                  id="pick_publisher"
                  defaultValue={selectedPublisher}
                  onChange={onPublisherChange}
                ></input>
                <datalist id="publishers">
                  {listPublisher.map((a) => {
                    return (
                      <option value={a.name} key={a.id}>
                        {a.name}
                      </option>
                    );
                  })}
                </datalist>
              </div>
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
            onClick={handleSubmitEditModal(onEditButtonClick)}
          >
            Save
          </button>
        </Modal.Footer>
      </Modal>

      {/* Edit  modal - Edit genre */}
      <Modal
        show={showEdit_EditGenreModal}
        onHide={handleCloseEdit_EditGenreModal}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa thể loại cho sản phẩm </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên thể loại : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="(chọn từ danh sách hoặc thêm mới)..."
                  list="genres"
                  id="pick_genre"
                ></input>
                <datalist id="genres">
                  {listGenre.map((genre) => {
                    return (
                      <option value={genre.name} key={genre.id}>
                        {genre.name}
                      </option>
                    );
                  })}
                </datalist>
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <button
            className="btn btn-danger"
            onClick={handleCloseEdit_EditGenreModal}
          >
            Close
          </button>
          <button className="btn btn-success" onClick={onEditGenreToProduct}>
            Thêm
          </button>
        </Modal.Footer>
      </Modal>
      {/* Edit  modal - Edit author */}
      <Modal
        show={showEdit_EditAuthorModal}
        onHide={handleCloseEdit_EditAuthorModal}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>Chỉnh sửa tác giả cho sản phẩm </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="form-group">
              <label>Tên tác giả : </label>
              <div className="input-group">
                <input
                  className="form-control"
                  placeholder="(chọn từ danh sách hoặc thêm mới)..."
                  list="genres"
                  id="pick_author"
                ></input>
                <datalist id="genres">
                  {listAuthor.map((a) => {
                    return (
                      <option value={a.name} key={a.id}>
                        {a.name}
                      </option>
                    );
                  })}
                </datalist>
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <button
            className="btn btn-danger"
            onClick={handleCloseEdit_EditAuthorModal}
          >
            Close
          </button>
          <button className="btn btn-success" onClick={onEditAuthorToProduct}>
            Thêm
          </button>
        </Modal.Footer>
      </Modal>
      {/* Delete  modal - Edit author */}
      <Modal
        show={showDeleteProductModal}
        onHide={handleCloseDeleteProductModal}
        size="lg"
      >
        <Modal.Header closeButton>
          <Modal.Title>Xóa sản phẩm </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p class="text-tron text-monospace">Xóa sản phẩm này?</p>
          <p class="text-center">
            <i class="fas fa-exclamation-triangle"></i>Bất cứ thông tin nào liên
            quan đến sản phẩm sẽ bị xóa!
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
          <button
            className="btn btn-danger"
            onClick={handleCloseDeleteProductModal}
          >
            Close
          </button>
          <button
            disabled={isDeleting}
            className="btn btn-success"
            onClick={onDelete_DeleteProductModal}
          >
            OK
          </button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
}

export default ProductTableItem;
