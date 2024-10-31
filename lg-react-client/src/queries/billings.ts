import {
  QueryKey,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { useApi } from "../contexts";
import { GetSummaryOutput, ListBillingsOutput } from "../api";

const billingsListingQueryKey: QueryKey = ["billingsListing"];
type BillingsListingReturnType = Array<ListBillingsOutput>;

const billingsAmountSummaryQueryKey: QueryKey = ["billingsAmountSummary"];
type BillingsAmountSummaryReturnType = GetSummaryOutput;

export function useListBillingsQuery() {
  const { workspaceApi } = useApi();
  return useQuery<BillingsListingReturnType>({
    queryKey: billingsListingQueryKey,
    queryFn: async () => await workspaceApi.listBillings({}),
  });
}

export function useGetSummaryQuery() {
  const { workspaceApi } = useApi();
  return useQuery<BillingsAmountSummaryReturnType>({
    queryKey: billingsAmountSummaryQueryKey,
    queryFn: async () => await workspaceApi.getSummary({}),
  });
}

export function useSetCheckedQuery(billingId: string) {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (checked: boolean) =>
      await workspaceApi.setChecked({
        id: billingId,
        checked,
      }),
    onSuccess: (res) => {
      queryClient.setQueryData<BillingsListingReturnType>(
        billingsListingQueryKey,
        (prev) => {
          if (!prev) return prev;
          const index = prev.findIndex((n) => n.id === billingId);
          return index < 0
            ? prev
            : [
                ...prev.slice(0, index),
                { ...prev[index], ...res },
                ...prev.slice(index + 1),
              ];
        }
      );
      queryClient.invalidateQueries({
        queryKey: billingsAmountSummaryQueryKey,
      });
    },
  });
}
