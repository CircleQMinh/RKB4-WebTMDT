import axios from "axios";
import { GetAPIUrl } from "./API";
const apiUrl = GetAPIUrl()
const token = localStorage.getItem("token");
const config = {
  headers: { Authorization: `Bearer ${token}` },
};

class AdminService {
  async GetOrdersForAdmin(status, orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/order?status=${status}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }
  async GetAllOrdersDetailsForOrder(id) {
    const response = await axios.get(
      `${apiUrl}/order/${id}/orderdetails`,
      config
    );
    return response;
  }
  //admin duyệt order
  async PutOrder(dto, id) {
    const response = await axios.put(`${apiUrl}/order/${id}`, dto, config);
    return response;
  }
  //admin xóa order
  async DeleteOrder(id) {
    const response = await axios.delete(`${apiUrl}/order/${id}`, config);
    return response;
  }

  //product --- book
  async GetBooksForAdmin(genre, orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/book?genre=${genre}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }

  //admin thêm product
  async AddProduct(product) {
    const response = await axios.post(`${apiUrl}/book`, product, config);
    return response;
  }
  //addmin edit product
  async EditProduct(id, product) {
    const response = await axios.put(`${apiUrl}/book/${id}`, product, config);
    return response;
  }
  //admin delete product
  async DeleteProduct(id) {
    const response = await axios.delete(`${apiUrl}/book/${id}`, config);
    return response;
  }

  //promotion

  async GetPromotionForAdmin(status, orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/promotion?status=${status}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }
  async GetPromotionInfosForAdmin(id) {
    const response = await axios.get(
      `${apiUrl}/Promotion/${id}/promotionInfos`,
      config
    );
    return response;
  }
  async PostPromotion(promo) {
    const response = await axios.post(`${apiUrl}/promotion`, promo, config);
    return response;
  }

  async PutPromotion(promo,id){
    const response = await axios.put(`${apiUrl}/promotion/${id}`, promo, config);
    return response;
  }
  async DeletePromotion(id){
    const response = await axios.delete(`${apiUrl}/promotion/${id}`, config);
    return response;
  }

  async PostPromotionInfo(promoInfo,promoid){
    const response = await axios.post(`${apiUrl}/promotion/${promoid}/promotionInfos`, promoInfo, config);
    return response;
  }
  async PutPromotionInfo(promoInfo,id){
    const response = await axios.put(`${apiUrl}/promotion/promotionInfos/${id}`, promoInfo, config);
    return response;
  }
  async DeletePromotionInfo(id){
    const response = await axios.delete(`${apiUrl}/promotion/promotionInfos/${id}`, config);
    return response;
  }

  async GetPromotableProduct() {
    const response = await axios.get(
      `${apiUrl}/Promotion/getPromotableProduct`,
      config
    );
    return response;
  }

  // -----user----------------
  async GetUserForAdmin( orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/user?orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }
  async AddUserForAdmin(user) {
    const response = await axios.post(
      `${apiUrl}/user`,
      user,
      config
    );
    return response;
  }
  async EditUserForAdmin(id,user) {
    const response = await axios.put(
      `${apiUrl}/user/${id}`,
      user,
      config
    );
    return response;
  }
  async DeleteUserForAdmin(id) {
    const response = await axios.delete(
      `${apiUrl}/user/${id}`,
      config
    );
    return response;
  }

  //Employee
  async GetEmpForAdmin(role,orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/Employee?role=${role}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }

  //Discount Code
  async GetDiscountCodeForAdmin(status,type,pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/Discount?status=${status}&type=${type}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }

  async PostDiscountCode(dcode){
    const response = await axios.post(`${apiUrl}/Discount`, dcode, config);
    return response;
  }
  async PutDiscountCode(dcode,id){
    const response = await axios.put(`${apiUrl}/Discount/${id}`, dcode, config);
    return response;
  }
  async DeleteDiscountCode(id){
    const response = await axios.delete(`${apiUrl}/Discount/${id}`, config);
    return response;
  }
  async PostDiscountCode(dcode){
    const response = await axios.post(`${apiUrl}/Discount/generateDiscountCode`, dcode, config);
    return response;
  }
}

export default new AdminService();
