import { TextField } from "@mui/material";
import {
  Control,
  FieldPathByValue,
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";
import { ErrorMessage } from "./ErrorMessage";
import { useState } from "react";

interface IProps<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPathByValue<
    TFieldValues,
    number | null
  > = FieldPathByValue<TFieldValues, number | null>
> extends UseControllerProps<TFieldValues, TName> {
  control: Control<TFieldValues>;
  label?: string | JSX.Element;
}

export const GNumericField = <TFieldValues extends FieldValues = FieldValues>({
  label,
  ...controllerProps
}: IProps<TFieldValues>) => {
  const [hasFocus, setHasFocus] = useState(false);
  const [internalValue, setInternalValue] = useState("");
  const { field, fieldState } = useController({
    ...controllerProps,
    defaultValue: controllerProps.defaultValue ?? (null as any),
  });

  const onChange = (
    evt: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
  ) => {
    const newValue: string = (evt.target.value ?? "").replace(",", ".");
    setInternalValue(newValue);
    const n = Number(newValue);
    field.onChange(newValue === "" || Number.isNaN(n) ? null : n);
  };

  return (
    <>
      {label}
      <TextField
        type="text"
        value={hasFocus ? internalValue : field.value?.toString() ?? ""}
        onChange={onChange}
        onFocus={() => {
          setInternalValue(field.value?.toString() ?? "");
          setHasFocus(true);
        }}
        onBlur={() => {
          field.onBlur();
          setHasFocus(false);
        }}
      />
      <ErrorMessage fieldState={fieldState} />
    </>
  );
};
