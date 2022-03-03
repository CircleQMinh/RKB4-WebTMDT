import axios from "axios";
import { GetAPIUrl } from "./API";
const apiUrl = GetAPIUrl()
const token = localStorage.getItem("token");
const config = {
  headers: { Authorization: `Bearer ${token}` },
};

class AuthService {
  async Login(email, password) {
    const response = await axios.post(`${apiUrl}/account/login`, {
      email: email,
      password: password,
    });
    return response;
  }
  async Register(data) {
    //console.log(data);
    const response = await axios.post(`${apiUrl}/account/register`, {
      email: data.email,
      password: data.password,
      imgUrl:
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/450px-No_image_available.svg.png",
      userName: data.userName,
      phoneNumber: data.phoneNumber,
      roles: ["User"],
    });
    return response;
  }
  async ConfimrEmail(email, token) {
    const response = await axios.post(`${apiUrl}/account/confirmEmail`, {
      email: email,
      token: token,
    });
    return response;
  }
  async RequestNewPassword(email) {
    const response = await axios.post(
      `${apiUrl}/account/requestNewPassword?email=${email}`
    );
    return response;
  }
  async ConfimrNewPassword(email, token, password) {
    const response = await axios.post(`${apiUrl}/account/confirmNewPassword`, {
      email: email,
      token: token,
      password: password,
    });
    return response;
  }
  async GetUserInfo(id) {
    const response = await axios.get(`${apiUrl}/account/${id}`);
    return response;
  }
  // async TestProvider(provider, url) {
  //   const response = await axios.post(`${apiUrl}?provider=${provider}&returnUrl=${url}`, {
  //     provider: provider,
  //     returnUrl: url,
  //   });
  //   return response;
  // }
  async GetAuthorizeAdmin() {
    const response = await axios.get(
      `${apiUrl}/account/getAuthorize/Administrator`,
      config
    );
    return response;
  }
  async GetAuthorizeShipper() {
    const response = await axios.get(
      `${apiUrl}/account/getAuthorize/Shipper`,
      config
    );
    return response;
  }
  async GetAuthorizeUser(id) {
    const response = await axios.get(
      `${apiUrl}/account/getAuthorize/User/${id}`,
      config
    );
    return response;
  }
}

export default new AuthService();
