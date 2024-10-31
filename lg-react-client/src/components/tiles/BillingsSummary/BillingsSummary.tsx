import { Box, CircularProgress } from "@mui/material";
import { useGetSummaryQuery } from "../../../queries/billings";

export const BillingsSummary = () => {
  const { data, isFetching } = useGetSummaryQuery();
  if (isFetching) return <CircularProgress />;
  if (!data) return null;
  return <Box>{data.amount} €</Box>;
};
