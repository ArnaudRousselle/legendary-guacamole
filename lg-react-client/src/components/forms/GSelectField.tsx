import {
  Control,
  FieldPath,
  FieldPathByValue,
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";
import { ErrorMessage } from "./ErrorMessage";
import { useEffect, useMemo } from "react";
import { HTMLSelect } from "@blueprintjs/core";

interface IProps<
  TItems extends FieldValues,
  TItemsPath extends FieldPath<TItems>,
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPathByValue<
    TFieldValues,
    TItems[TItemsPath] | null
  > = FieldPathByValue<TFieldValues, TItems[TItemsPath] | null>
> extends UseControllerProps<TFieldValues, TName> {
  data: TItems[];
  control: Control<TFieldValues>;
  labelKey: keyof TItems;
  keyProps: TItemsPath;
  label?: string | JSX.Element;
}

export const GSelectField = <
  TItems extends FieldValues,
  TItemsPath extends FieldPath<TItems>,
  TFieldValues extends FieldValues = FieldValues
>({
  data,
  labelKey,
  keyProps,
  label,
  ...controllerProps
}: IProps<TItems, TItemsPath, TFieldValues>) => {
  const defaultValue = controllerProps.defaultValue ?? (null as any);
  const { field, fieldState } = useController({
    ...controllerProps,
    defaultValue,
  });

  const { onChange: setFieldValue } = field;

  const handleChange = (evt: React.ChangeEvent<HTMLSelectElement>) => {
    console.log(evt.target.value);
    setFieldValue(evt.target.value);
  };

  const selectedValue = useMemo(
    () => data.find((item) => item && item[keyProps] === field.value) ?? null,
    [data, field.value, keyProps]
  );

  const needToReset = selectedValue === null && field.value !== null;

  useEffect(() => {
    if (needToReset) setFieldValue(null as any);
  }, [setFieldValue, needToReset, defaultValue]);

  return (
    <>
      {label}
      <HTMLSelect
        value={field.value ?? ""}
        onChange={handleChange}
        onBlur={field.onBlur}
      >
        {data.map((item) => (
          <option key={item[keyProps]} value={item[keyProps]}>
            {item[labelKey]}
          </option>
        ))}
      </HTMLSelect>
      <button type="button" onClick={() => setFieldValue(null)}>
        X
      </button>
      <ErrorMessage fieldState={fieldState} />
    </>
  );
};
