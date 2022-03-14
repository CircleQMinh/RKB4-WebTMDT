import React from "react";
import Modal from "react-bootstrap/Modal";
import Pagination from "../../../shared-components/Pagination";

import OrderTableItem from "../../Admin/TableItem/Order";


function OrderHistoryModal(props) {
  var data = props.listOrderHistory;
  //console.log(data);

  function onPageNumberChange(event) {
    var pageNum = event.target.value;
    props.GetOrderHistory(pageNum, 4);
  }
  return (
    <Modal
      show={props.showOrderHistoryModal}
      onHide={props.handleCloseOrderHistoryModal}
      size="xl"
    >
      <Modal.Header closeButton>
        <Modal.Title>Lịch sử đơn hàng</Modal.Title>
      </Modal.Header>
      <Modal.Body style={{ backgroundColor: "#1E1E28" }}>
        {props.isGettingOrderHistory && (
          <div className="d-flex justify-content-center">
            <div className="spinner-border text-info" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="text monospace ms-2 text-white">
              Đang xử lý xin chờ tí...
            </p>
          </div>
        )}
       
        {!props.isGettingOrderHistory && data.length > 0 && (
          <div className="table-responsive ">
            <table className="table">
              <thead className="text-primary">
                <tr>
                  <th className="text-center">#</th>
                  <th>Thông tin liên lạc</th>
                  <th>Tổng giá</th>
                  <th>Thanh toán</th>
                  <th>Trạng thái</th>
                  <th className="text-right">Ngày đặt</th>
                  <th className="text-right">Actions</th>
                </tr>
              </thead>

              <tbody>
                {data.map((item, i) => {
                  return (
                    <OrderTableItem
                      item={item}
                      key={i}
                      reRender={null}
                    ></OrderTableItem>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}

        {!props.isGettingOrderHistory && data.length == 0 && (
          <p className="text-center text-white">
            Không tìm thấy thông tin tìm kiếm!
          </p>
        )}
        {!props.isGettingOrderHistory && data.length != 0 && (
          <div className="d-flex justify-content-center">
            <Pagination
              totalPage={props.totalOrderHistoryPage}
              onPageNumberChange={onPageNumberChange}
              pageNumber={props.currentOrderHistoryPage}
            ></Pagination>
          </div>
        )}
      </Modal.Body>

      <Modal.Footer>
        <button
          className="btn btn-danger"
          onClick={props.handleCloseOrderHistoryModal}
        >
          Close
        </button>
      </Modal.Footer>
    </Modal>
  );
}

export default OrderHistoryModal;
