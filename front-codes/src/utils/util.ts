import app from "../service/service";

export const getUsers = async (
  setAllUsers: React.Dispatch<
    React.SetStateAction<
      {
        userId: number;
        firstName: string;
        lastName: string;
        phoneNumber: string;
      }[]
    >
  >
) => {
  try {
    const { data } = await app("/User/GetUsers");
    console.log(data);
    setAllUsers(data.data);
  } catch (error) {
    console.log(error);
  }
};
