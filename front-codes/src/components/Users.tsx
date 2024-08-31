import { useEffect, useState } from "react";
import app from "../service/service";
import EditUserForm from "./EditUserForm";

function Users() {
  const [allUsers, setAllUsers] = useState([]);
  const [showEditForm, setShowEditForm] = useState({
    show: false,
    userId: "",
  });

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
    <div className="text-center">
      <div
        className={`${
          showEditForm.show
            ? "opacity-100 transition-all max-h-[1000px] duration-1000 ease-in"
            : "opacity-0 transition-all max-h-0 duration-1000 ease-in"
        } overflow-hidden`}
      >
        <EditUserForm
          userId={Number(showEditForm.userId)}
          setShowForm={setShowEditForm}
        />
      </div>

      <div className="grid grid-cols-5 justify-center items-center font-semibold my-4">
        <p>ردیف</p>
        <p>نام</p>
        <p>نام خانوادگی</p>
        <p>شماره همراه</p>
        <p>عملیات</p>
      </div>
      <div className="grid grid-cols-1 gap-3">
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
            <div
              key={item.userId}
              className="grid grid-cols-5 justify-center items-center"
            >
              <p className="font-semibold">{index + 1}</p>
              <p>{item.firstName}</p>
              <p>{item.lastName}</p>
              <p>{item.phoneNumber}</p>
              <p className="flex flex-row justify-center w-full gap-3">
                <button
                  className="bg-red-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                  onClick={() => deleteUser(item.userId)}
                >
                  حذف کاربر
                </button>
                <button
                  className="bg-green-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                  onClick={() =>
                    setShowEditForm({
                      show: true,
                      userId: item.userId.toString(),
                    })
                  }
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
