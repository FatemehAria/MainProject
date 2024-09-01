import { FormInputProps } from "../types/type";

function FormInput({
  label,
  value,
  name,
  onChange,
  pattern,
  placeholder,
  type,
  autoFocus,
  onBlur,
}: FormInputProps) {
  return (
    <div className="relative">
      <label className="absolute -top-3 right-4 z-10 lg:text-[16px] lg:px-2 px-2 bg-white text-[#000000]">
        {label}
      </label>
      <input
        className="border-[0.3px] border-indigo-500 text-[#000000] mx-auto outline-none rounded-md px-2 py-2 text-lg w-full "
        placeholder={placeholder}
        type={type}
        onChange={onChange}
        value={value}
        autoComplete="off"
        pattern={pattern}
        name={name}
        autoFocus={autoFocus}
        onBlur={onBlur}
      />
    </div>
  );
}

export default FormInput;
