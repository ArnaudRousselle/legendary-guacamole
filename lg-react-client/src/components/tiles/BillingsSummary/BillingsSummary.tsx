import { Spinner } from "@blueprintjs/core";
import { useGetSummaryQuery } from "../../../queries/billings";

export const BillingsSummary = () => {
  const { data, isFetching } = useGetSummaryQuery();
  if (isFetching) return <Spinner />;
  if (!data) return null;
  return <div>{data.amount} €</div>;
};
