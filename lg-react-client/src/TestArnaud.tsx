import { useQuery } from "@tanstack/react-query";
import { useApi } from "./contexts";
import { useForm } from "react-hook-form";

//todo ARNAUD: à conserver
type ShortDate = {
  year: number;
  month: number;
  day: number;
};

type FormData = {
  date: ShortDate | null;
  name: string;
  amount: number | null;
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
      <form onSubmit={handleSubmit((v) => console.log("submit", v))}></form>
    </>
  );
};
