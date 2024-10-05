import { useQuery } from "@tanstack/react-query";
import { useApi } from "./contexts";
import { useForm } from "react-hook-form";
import { GTextField } from "./components/forms/GTextField";
import { GCheckBoxField } from "./components/forms/GCheckBoxField";
import { GNumericField } from "./components/forms/GNumericField";
import { GDateField, GSelectField, ShortDate } from "./components/forms";

type FormData = {
  date: ShortDate | null;
  name: string;
  amount: number | null;
  isOk: boolean;
  typeStr: string | null;
  typeNumber: number | null;
};

export const TestArnaud = () => {
  const { legendaryGuacamoleWebApiApi } = useApi();
  const { data, isFetching, isSuccess } = useQuery({
    queryKey: ["testArnaud"],
    queryFn: async () => await legendaryGuacamoleWebApiApi.getWeatherForecast(),
  });

  const { control, handleSubmit } = useForm<FormData>({
    mode: "onBlur",
    defaultValues: {
      date: null,
      name: "",
      amount: null,
    },
  });

  if (isFetching) return "loading...";
  if (!isSuccess) return "error";
  return (
    <>
      {data.map((d, i) => (
        <div key={i}>{d.summary}</div>
      ))}
      <div>
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
        <button
          type="button"
          onClick={() => handleSubmit((v) => console.log("submit", v))()}
        >
          ok
        </button>
      </div>
    </>
  );
};
