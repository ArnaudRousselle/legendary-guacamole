import { TextField } from "@mui/material";
import {
  Control,
  FieldPathByValue,
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";
import { ErrorMessage } from "./ErrorMessage";

interface IProps<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPathByValue<TFieldValues, string> = FieldPathByValue<
    TFieldValues,
    string
  >
> extends UseControllerProps<TFieldValues, TName> {
  control: Control<TFieldValues>;
  label?: string | JSX.Element;
}

export const GTextField = <TFieldValues extends FieldValues = FieldValues>({
  label,
  ...controllerProps
}: IProps<TFieldValues>) => {
  const { field, fieldState } = useController({
    ...controllerProps,
    defaultValue: controllerProps.defaultValue ?? ("" as any),
  });

  return (
    <>
      <TextField
        label={label}
        type="text"
        value={field.value ?? ""}
        onChange={(evt) => field.onChange(evt.target.value)}
        onBlur={field.onBlur}
      />
      <ErrorMessage fieldState={fieldState} />
    </>
  );
};
