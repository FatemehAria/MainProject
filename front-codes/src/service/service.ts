import axios from "axios";

const app = axios.create({
  baseURL: "https://localhost:44334",
  headers: {
    "Content-Type": "application/json",
  },
});
export default app;
