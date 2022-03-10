export function GetAPIUrl() {
  const apiUrl = "https://localhost:7099/api";
  return apiUrl;
}
export function GetConfig() {
  const token = localStorage.getItem("token");
  const config = {
    headers: { Authorization: `Bearer ${token}` },
  };
  return config
}
