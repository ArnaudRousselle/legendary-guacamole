import { Checkbox } from "@mui/material";
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
  TName extends FieldPathByValue<TFieldValues, boolean> = FieldPathByValue<
    TFieldValues,
    boolean
  >
> extends UseControllerProps<TFieldValues, TName> {
  control: Control<TFieldValues>;
  label?: string | JSX.Element;
}

export const GCheckBoxField = <TFieldValues extends FieldValues = FieldValues>({
  label,
  ...controllerProps
}: IProps<TFieldValues>) => {
  const { field, fieldState } = useController({
    ...controllerProps,
    defaultValue: controllerProps.defaultValue ?? (false as any),
  });

  return (
    <>
      {label}
      <Checkbox
        checked={field.value}
        onChange={(evt) => field.onChange(evt.target.checked)}
        onBlur={field.onBlur}
      />
      <ErrorMessage fieldState={fieldState} />
    </>
  );
};
