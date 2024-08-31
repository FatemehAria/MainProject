export type FormInputProps = {
  label?: string;
  placeholder?: string;
  type: string;
  pattern?: string;
  name?: string;
  onChange: React.ChangeEventHandler<HTMLInputElement>;
  value: string;
};
export type SubmissionBtnProps = {
  text: string;
  // validation?: boolean;
  type?: "submit" | "button" | "reset" | undefined;
};