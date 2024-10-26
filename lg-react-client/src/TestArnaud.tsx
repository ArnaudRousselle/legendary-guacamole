import { useForm } from "react-hook-form";
import { GTextField } from "./components/forms/GTextField";
import { GCheckBoxField } from "./components/forms/GCheckBoxField";
import { GNumericField } from "./components/forms/GNumericField";
import { GDateField, GSelectField, ShortDate } from "./components/forms";
import { Box, Button } from "@mui/material";

type FormData = {
  date: ShortDate | null;
  name: string;
  amount: number | null;
  isOk: boolean;
  typeStr: string | null;
  typeNumber: number | null;
};

export const TestArnaud = () => {
  const { control, handleSubmit } = useForm<FormData>({
    mode: "onBlur",
    defaultValues: {
      date: null,
      name: "",
      amount: null,
    },
  });

  return (
    <>
      <Box>
        <GTextField control={control} name="name" rules={{ required: true }} />
        <GCheckBoxField control={control} name="isOk" />
        <GNumericField control={control} name="amount" />
        <GDateField control={control} name="date" />
        <GSelectField
          control={control}
          data={[
            { id: "A", label: "aaa" },
            { id: "B", label: "bbb" },
            { id: "C", label: "ccc" },
          ]}
          labelKey="label"
          keyProps="id"
          name="typeStr"
        />
        <GSelectField
          control={control}
          data={[
            { id: 1, label: "111" },
            { id: 2, label: "222" },
            { id: 3, label: "333" },
          ]}
          labelKey="label"
          keyProps="id"
          name="typeNumber"
        />
        <Button
          type="button"
          onClick={() => handleSubmit((v) => console.log("submit", v))()}
        >
          ok
        </Button>
      </Box>
    </>
  );
};
