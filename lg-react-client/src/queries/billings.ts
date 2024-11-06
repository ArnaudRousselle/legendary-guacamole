import {
  QueryKey,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { useApi } from "../contexts";
import {
  AddBillingInput,
  DeleteBillingInput,
  EditBillingInput,
  SetCheckedInput,
} from "../api";
import { PromiseReturnType } from "../utils/PromiseReturnType";

const billingsListingQueryKey: QueryKey = ["billingsListing"];
const getBillingQueryKey: QueryKey = ["getBilling"];
const billingsAmountSummaryQueryKey: QueryKey = ["billingsAmountSummary"];

export function useListBillingsQuery() {
  const { workspaceApi } = useApi();
  return useQuery({
    queryKey: billingsListingQueryKey,
    queryFn: async () => await workspaceApi.listBillings({}),
  });
}

export function useGetBillingQuery(id: string) {
  const { workspaceApi } = useApi();
  return useQuery({
    queryKey: getBillingQueryKey,
    queryFn: async () => await workspaceApi.getBilling({ id }),
  });
}

export function useGetSummaryQuery() {
  const { workspaceApi } = useApi();
  return useQuery({
    queryKey: billingsAmountSummaryQueryKey,
    queryFn: async () => await workspaceApi.getSummary({}),
  });
}

export function useAddBillingQuery() {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (values: AddBillingInput) =>
      await workspaceApi.addBilling(values),
    onSuccess: (res) => {
      queryClient.setQueryData<
        PromiseReturnType<typeof workspaceApi.listBillings>
      >(billingsListingQueryKey, (prev) => {
        if (!prev) return prev;
        return [...prev, res];
      });
      queryClient.invalidateQueries({
        queryKey: billingsAmountSummaryQueryKey,
      });
    },
  });
}

export function useEditBillingQuery() {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (values: EditBillingInput) =>
      await workspaceApi.editBilling(values),
    onSuccess: (res) => {
      queryClient.setQueryData<
        PromiseReturnType<typeof workspaceApi.listBillings>
      >(billingsListingQueryKey, (prev) => {
        if (!prev) return prev;
        const index = prev.findIndex((n) => n.id === res.id);
        return index < 0
          ? prev
          : [...prev.slice(0, index), res, ...prev.slice(index + 1)];
      });
      queryClient.invalidateQueries({
        queryKey: billingsAmountSummaryQueryKey,
      });
    },
  });
}

export function useDeleteBillingQuery() {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (values: DeleteBillingInput) => {
      await workspaceApi.deleteBilling(values);
      return values;
    },
    onSuccess: (res) => {
      queryClient.setQueryData<
        PromiseReturnType<typeof workspaceApi.listBillings>
      >(billingsListingQueryKey, (prev) => {
        if (!prev) return prev;
        const index = prev.findIndex((n) => n.id === res.id);
        return index < 0
          ? prev
          : [...prev.slice(0, index), ...prev.slice(index + 1)];
      });
      queryClient.invalidateQueries({
        queryKey: billingsAmountSummaryQueryKey,
      });
    },
  });
}

export function useSetCheckedQuery() {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (values: SetCheckedInput) =>
      await workspaceApi.setChecked(values),
    onSuccess: (res) => {
      queryClient.setQueryData<
        PromiseReturnType<typeof workspaceApi.listBillings>
      >(billingsListingQueryKey, (prev) => {
        if (!prev) return prev;
        const index = prev.findIndex((n) => n.id === res.id);
        return index < 0
          ? prev
          : [
              ...prev.slice(0, index),
              { ...prev[index], ...res },
              ...prev.slice(index + 1),
            ];
      });
      queryClient.invalidateQueries({
        queryKey: billingsAmountSummaryQueryKey,
      });
    },
  });
}
