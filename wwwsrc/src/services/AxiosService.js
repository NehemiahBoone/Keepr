import Axios from "axios";

let baseUrl = location.host.includes("localhost")
  ? "http://localhost:3000"
  : "/";

export const api = Axios.create({
  baseURL: baseUrl + "api",
  timeout: 3000
});

export const setBearer = function (bearer) {
  api.defaults.headers.authorization = bearer;
};

export const resetBearer = function () {
  api.defaults.headers.authorization = "";
};
