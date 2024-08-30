export type FormInputProps = {
  label?: string;
  placeholder?: string;
  type?: string;
  pattern?: string;
  name?: string;
  onChange?: any;
  value: string;
  onBlur?: any;
  autoFocus?: boolean;
};
export type SubmissionBtnProps = {
  text: string;
  // validation?: boolean;
  type?: "submit" | "button" | "reset" | undefined;
};