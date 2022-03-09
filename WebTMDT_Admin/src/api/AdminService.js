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
      `${apiUrl}/product?genre=${genre}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      config
    );
    return response;
  }

  //admin thêm product
  async AddProduct(product) {
    const response = await axios.post(`${apiUrl}/product`, product, config);
    return response;
  }
  //addmin edit product
  async EditProduct(id, product) {
    const response = await axios.put(`${apiUrl}/product/${id}`, product, config);
    return response;
  }
  //admin delete product
  async DeleteProduct(id) {
    const response = await axios.delete(`${apiUrl}/product/${id}`, config);
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
  //Search
  async GetSearchResult(type,by,key,pageNumber,pageSize){
    const response = await axios.get(`${apiUrl}/statistic/search?type=${type}&searchBy=${by}&keyword=${key}&pageNumber=${pageNumber}&pageSize=${pageSize}`, config);
    return response;
  }

}

export default new AdminService();
