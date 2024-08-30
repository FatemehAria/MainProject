import { useEffect } from "react";
import app from "../service/service";

function Users() {
  const getUsers = async () => {
    try {
      const { data } = await app("/User/GetUsers");
      console.log(data);
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    getUsers();
  }, []);
  return <div>Users</div>;
}

export default Users;
