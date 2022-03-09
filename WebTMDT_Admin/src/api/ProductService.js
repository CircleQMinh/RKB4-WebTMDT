import axios from "axios";

import { GetAPIUrl } from "./API";
const apiUrl = GetAPIUrl()

class ProductService {

  // --------------------------------------------------------------------------------------------------------

  async getGenre() {
    const response = await axios.get(`${apiUrl}/Genre`);
    return response;
  }
  async getAuthor() {
    const response = await axios.get(`${apiUrl}/Author`);
    return response;
  }
  async getPublisher() {
    const response = await axios.get(`${apiUrl}/Publisher`);
    return response;
  }

  // -----------------------------------------------------------------------------------------------------------


}

export default new ProductService();