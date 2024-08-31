import app from "../service/service";

export const getUsers = async (
  setAllUsers: React.Dispatch<React.SetStateAction<never[]>>
) => {
  try {
    const { data } = await app("/User/GetUsers");
    console.log(data);
    setAllUsers(data.data);
  } catch (error) {
    console.log(error);
  }
};
