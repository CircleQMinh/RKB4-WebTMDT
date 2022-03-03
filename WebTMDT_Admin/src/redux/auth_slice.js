import { createSlice } from "@reduxjs/toolkit";
import { formatDate } from "../helper/formatDate";
const initialState = { user: {}, isLoggedIn: false };
const auth_slice = createSlice({
  name: "auth_slice",
  initialState: initialState,
  reducers: {
    saveAuthInfoToLocalStorage(state, action) {
      state.user = action.payload.user;
      state.isLoggedin = true;
   
      localStorage.setItem("user", JSON.stringify(action.payload.user));
      localStorage.setItem("isLoggedIn", "true");
      localStorage.setItem("token", action.payload.token);
      let a = new Date();
      a.setMinutes(a.getMinutes() + 120);
      localStorage.setItem(
        "loginTimeOut",
        formatDate(a, "yyyy-MM-dd HH:mm:ss")
      );
    },
    toggelIsLoggedIn(state, action) {
      state.isLoggedIn = !state.isLoggedIn;
    },
    getAuthInfoFromLocalStorage(state, action) {
      let u = localStorage.getItem("user");
      if (u !== null) {
        //console.log("Tìm thấy phiên đăng nhập!");
        let timeOut = new Date(localStorage.getItem("loginTimeOut"));
        let timeNow = new Date();
        // console.log(timeOut);
        // console.log(timeNow);
        if (timeOut.getTime() < timeNow.getTime()) {
          localStorage.removeItem("user");
          localStorage.removeItem("isLoggedIn");
          localStorage.removeItem("token");
          localStorage.removeItem("loginTimeOut")
         // console.log("Hết phiên đăng nhập! Xóa hết info cũ");
        } else {
          let user = JSON.parse(u)
          state.user = user;
          state.isLoggedIn = true;
          //console.log("Còn phiên đăng nhập! Dùng lại info cũ");
        }
      } else {
        //console.log("Không tìm thấy phiên đăng nhập nào");
      }
    },
    logOut(state, action) {
      localStorage.removeItem("user");
      localStorage.removeItem("isLoggedIn");
      localStorage.removeItem("token");
      localStorage.removeItem("loginTimeOut")
      console.log("Đăng xuất! Xóa hết info cũ");
      window.location.reload()
    },
  },
});

export const auth_action = auth_slice.actions;

export default auth_slice;
