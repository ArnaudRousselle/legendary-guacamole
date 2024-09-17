import { useQuery } from "@tanstack/react-query";
import { useApi } from "./contexts";

export const TestArnaud = () => {
  const { legendaryGuacamoleWebApiApi } = useApi();
  const { data, isFetching, isSuccess } = useQuery({
    queryKey: ["testArnaud"],
    queryFn: async () => await legendaryGuacamoleWebApiApi.getWeatherForecast(),
  });

  if (isFetching) return "loading...";
  if (!isSuccess) return "error";
  return (
    <>
      {data.map((d, i) => (
        <div key={i}>{d.summary}</div>
      ))}
    </>
  );
};
