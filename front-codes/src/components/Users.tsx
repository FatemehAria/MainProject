import { useEffect, useState } from "react";
import app from "../service/service";
import EditUserForm from "./EditUserForm";
import { getUsers } from "../utils/util";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

function Users() {
  const localToken = sessionStorage.getItem("token") as string;
  const navigate = useNavigate();
  const [userStatus, setUsersStatus] = useState({
    loading: false,
    errorMsg: "",
  });
  const [allUsers, setAllUsers] = useState<
    {
      userId: number;
      firstName: string;
      lastName: string;
      phoneNumber: string;
    }[]
  >([]);
  const [showEditForm, setShowEditForm] = useState({
    show: false,
    userId: "",
  });

  const handleToDelete = async (userId: number) => {
    try {
      const { data } = await app.post("/User/DeleteUserById", userId, {
        headers: {
          Authorization: `Bearer ${localToken}`,
        },
      });
      if (data.success) {
        setAllUsers((prevUsers) =>
          prevUsers.filter((user: { userId: number }) => user.userId !== userId)
        );
        toast.success("کاربر با موفقیت حذف شد.");
      }
      console.log("delete", data);
    } catch (error) {
      toast.error("خطا در حذف کاربر.");
      console.log(error);
    }
  };

  const handleLogout = () => {
    sessionStorage.clear();
    navigate("/");
  };
  useEffect(() => {
    getUsers(setAllUsers, setUsersStatus, localToken);
  }, []);

  if (localToken) {
    return (
      <div className="">
        <button
          className="bg-blue-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 my-3 rounded-[7px] font-semibold"
          onClick={() => handleLogout()}
        >
          خروج
        </button>
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
            setAllUsers={setAllUsers}
            allUsers={allUsers}
          />
        </div>

        <div className="grid lg:grid-cols-5 grid-cols-10 justify-center items-center font-semibold my-4 relative text-center">
          <p className="col-span-1">ردیف</p>
          <p className="col-span-2 lg:col-span-1">نام</p>
          <p className="col-span-3 lg:col-span-1">نام خانوادگی</p>
          <p className="col-span-2 lg:col-span-1">شماره همراه</p>
          <p className="col-span-2 lg:col-span-1">عملیات</p>
        </div>
        <div className="grid grid-cols-1 gap-3 text-center">
          {userStatus.loading ? (
            <div className="absolute left-1/2 -translate-x-1/2 top-1/2 -translate-y-1/2">
              <div className="loader"></div>
            </div>
          ) : allUsers.length > 0 ? (
            allUsers?.map(
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
                  className={`grid lg:grid-cols-5 grid-cols-10 justify-center items-center ${
                    item.userId === +showEditForm.userId
                      ? "bg-yellow-400 rounded-lg py-2 text-white text-lg"
                      : ""
                  }`}
                >
                  <p className="font-semibold col-span-1">{index + 1}</p>
                  <p className="col-span-2 lg:col-span-1">{item.firstName}</p>
                  <p className="col-span-3 lg:col-span-1">{item.lastName}</p>
                  <p className="col-span-2 lg:col-span-1">{item.phoneNumber}</p>
                  <p className="flex lg:flex-row flex-col justify-center w-full gap-3 col-span-2 lg:col-span-1">
                    <button
                      className="bg-red-600 cursor-pointer w-[100px] text-white whitespace-nowrap py-1 rounded-[7px] font-semibold"
                      onClick={() => handleToDelete(item.userId)}
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
            )
          ) : (
            <p className="bg-red-500 text-white text-lg font-semibold rounded-md py-1">کاربری یافت نشد.</p>
          )}
        </div>
      </div>
    );
  } else {
    return <div></div>;
  }
}

export default Users;
