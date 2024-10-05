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
  TName extends FieldPathByValue<
    TFieldValues,
    ShortDate | null
  > = FieldPathByValue<TFieldValues, ShortDate | null>
> extends UseControllerProps<TFieldValues, TName> {
  control: Control<TFieldValues>;
  label?: string | JSX.Element;
}

export type ShortDate = {
  day: number;
  month: number;
  year: number;
};

function formatToDateString(date: ShortDate | null): string {
  if (!date) return "";
  return (
    date.year.toString().padStart(4, "0") +
    "-" +
    date.month.toString().padStart(2, "0") +
    "-" +
    date.day.toString().padStart(2, "0")
  );
}

export const GDateField = <TFieldValues extends FieldValues = FieldValues>({
  label,
  ...controllerProps
}: IProps<TFieldValues>) => {
  const { field, fieldState } = useController({
    ...controllerProps,
    defaultValue: controllerProps.defaultValue ?? ("" as any),
  });

  const onChange = (
    evt: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
  ) => {
    const str = evt.target.value;
    if (!str) field.onChange(null);
    else {
      const args = str.split("-");
      const newValue: ShortDate = {
        day: Number(args[2]),
        month: Number(args[1]),
        year: Number(args[0]),
      };
      field.onChange(newValue);
    }
  };

  return (
    <>
      {label}
      <TextField
        type="date"
        value={formatToDateString(field.value)}
        onChange={onChange}
        onBlur={field.onBlur}
      />
      <button type="button" onClick={() => field.onChange(null)}>
        X
      </button>
      <ErrorMessage fieldState={fieldState} />
    </>
  );
};
