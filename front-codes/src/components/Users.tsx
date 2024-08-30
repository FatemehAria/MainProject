import { useEffect, useState } from "react";
import app from "../service/service";

function Users() {
  const [allUsers, setAllUsers] = useState([]);

  const getUsers = async () => {
    try {
      const { data } = await app("/User/GetUsers");
      console.log(data);
      setAllUsers(data.data);
    } catch (error) {
      console.log(error);
    }
  };

  const deleteUser = async (userId: number) => {
    try {
      const { data } = await app.post("/User/DeleteUserById", {
        userId,
      });
      console.log(data);
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    getUsers();
  }, []);
  return (
    <div>
      <div className="grid grid-cols-5">
        <p>ردیف</p>
        <p>نام</p>
        <p>نام خانوادگی</p>
        <p>شماره همراه</p>
        <p>عملیات</p>
      </div>
      <div>
        {allUsers?.map(
          (
            item: {
              userId: number;
              firstName: string;
              lastName: string;
              phoneNumber: string;
            },
            index
          ) => (
            <div key={item.userId} className="grid grid-cols-5">
              <p>{index + 1}</p>
              <p>{item.firstName}</p>
              <p>{item.lastName}</p>
              <p>{item.phoneNumber}</p>
              <p className="flex flex-row justify-center w-full">
                <button
                  className="bg-red-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                  onClick={() => deleteUser(item.userId)}
                >
                  حذف کاربر
                </button>
                <button
                  className="bg-green-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                  onClick={() => deleteUser(item.userId)}
                >
                  ویرایش کاربر
                </button>
              </p>
            </div>
          )
        )}
      </div>
    </div>
  );
}

export default Users;
