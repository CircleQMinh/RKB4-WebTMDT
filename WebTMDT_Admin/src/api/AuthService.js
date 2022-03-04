import axios from "axios";
import { GetAPIUrl } from "./API";
const apiUrl = GetAPIUrl();

class AuthService {
  async Login(email, password) {
    const response = await axios.post(`${apiUrl}/account/login`, {
      email: email,
      password: password,
    });
    return response;
  }

  async GetAuthorizeAdmin() {
    const token = localStorage.getItem("token");
    const config = {
      headers: { Authorization: `Bearer ${token ? token : null}` },
    };
    const response = await axios.get(
      `${apiUrl}/account/getAuthorize/Administrator`,
      config
    );
    return response;
  }
}

export default new AuthService();
