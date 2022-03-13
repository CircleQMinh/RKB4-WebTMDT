import axios from "axios";
import { GetAPIUrl,GetConfig } from "./API";
const apiUrl = GetAPIUrl()

class AdminService {
  async GetOrdersForAdmin(status, orderBy, sort, pageNumber, pageSize) { 
    const response = await axios.get(
      `${apiUrl}/order?status=${status}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      GetConfig()
    );
    return response;
  }
  async GetAllOrdersDetailsForOrder(id) {
    const response = await axios.get(
      `${apiUrl}/order/${id}/orderdetails`,
      GetConfig()
    );
    return response;
  }
  //admin duyệt order
  async PutOrder(dto, id) {
    const response = await axios.put(`${apiUrl}/order/${id}`, dto,  GetConfig());
    return response;
  }
  //admin xóa order
  async DeleteOrder(id) {
    const response = await axios.delete(`${apiUrl}/order/${id}`,  GetConfig());
    return response;
  }

  //product --- book
  async GetBooksForAdmin(genre, orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/product?genre=${genre}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      GetConfig()
    );
    return response;
  }

  //admin thêm product
  async AddProduct(product) {
    const response = await axios.post(`${apiUrl}/product`, product,  GetConfig());
    return response;
  }
  //addmin edit product
  async EditProduct(id, product) {
    const response = await axios.put(`${apiUrl}/product/${id}`, product,  GetConfig());
    return response;
  }
  //admin delete product
  async DeleteProduct(id) {
    const response = await axios.delete(`${apiUrl}/product/${id}`,  GetConfig());
    return response;
  }

  //promotion

  async GetPromotionForAdmin(status, orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/promotion?status=${status}&orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      GetConfig()
    );
    return response;
  }
  async GetPromotionInfosForAdmin(id) {
    const response = await axios.get(
      `${apiUrl}/Promotion/${id}/promotionInfos`,
      GetConfig()
    );
    return response;
  }
  async PostPromotion(promo) {
    const response = await axios.post(`${apiUrl}/promotion`, promo,  GetConfig());
    return response;
  }

  async PutPromotion(promo,id){
    const response = await axios.put(`${apiUrl}/promotion/${id}`, promo,  GetConfig());
    return response;
  }
  async DeletePromotion(id){
    const response = await axios.delete(`${apiUrl}/promotion/${id}`,  GetConfig());
    return response;
  }

  async PostPromotionInfo(promoInfo,promoid){
    const response = await axios.post(`${apiUrl}/promotion/${promoid}/promotionInfos`, promoInfo,  GetConfig());
    return response;
  }
  async PutPromotionInfo(promoInfo,id){
    const response = await axios.put(`${apiUrl}/promotion/promotionInfos/${id}`, promoInfo,  GetConfig());
    return response;
  }
  async DeletePromotionInfo(id){
    const response = await axios.delete(`${apiUrl}/promotion/promotionInfos/${id}`,  GetConfig());
    return response;
  }

  async GetPromotableProduct() {
    const response = await axios.get(
      `${apiUrl}/Promotion/getPromotableProduct`,
      GetConfig()
    );
    return response;
  }

  // -----user----------------
  async GetUserForAdmin( orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/user?orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      GetConfig()
    );
    return response;
  }
  async AddUserForAdmin(user) {
    const response = await axios.post(
      `${apiUrl}/user`,
      user,
      GetConfig()
    );
    return response;
  }
  async EditUserForAdmin(id,user) {
    const response = await axios.put(
      `${apiUrl}/user/${id}`,
      user,
      GetConfig()
    );
    return response;
  }
  async DeleteUserForAdmin(id) {
    const response = await axios.delete(
      `${apiUrl}/user/${id}`,
      GetConfig()
    );
    return response;
  }
  //Search
  async GetSearchResult(type,by,key,pageNumber,pageSize){
    const response = await axios.get
    (`${apiUrl}/statistic/search?type=${type}&searchBy=${by}&keyword=${key}&pageNumber=${pageNumber}&pageSize=${pageSize}`,  GetConfig());
    return response;
  }
  async GetSearchResult_User(by,key,pageNumber,pageSize){
    const response = await axios.get
    (`${apiUrl}/statistic/search/user?searchBy=${by}&keyword=${key}&pageNumber=${pageNumber}&pageSize=${pageSize}`,  GetConfig());
    return response;
  }

  //Genre
  async GetGenreForAdmin( orderBy, sort, pageNumber, pageSize) {
    const response = await axios.get(
      `${apiUrl}/Genre/details?orderby=${orderBy}&sort=${sort}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      GetConfig()
    );
    return response;
  }
  async AddGenreForAdmin(o) {
    const response = await axios.post(
      `${apiUrl}/genre`,
      o,
      GetConfig()
    );
    return response;
  }
  async EditGenreForAdmin(id,o) {
    const response = await axios.put(
      `${apiUrl}/genre/${id}`,
      o,
      GetConfig()
    );
    return response;
  }
  async DeleteGenreForAdmin(id) {
    const response = await axios.delete(
      `${apiUrl}/genre/${id}`,
      GetConfig()
    );
    return response;
  }

  //Dashboard

  async GetDashboardInfo(){
    const response = await axios.get(`${apiUrl}/Statistic/DashboardInfo`, GetConfig());
    return response;
  }
}

export default new AdminService();
