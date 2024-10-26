import { Checkbox, CircularProgress } from "@mui/material";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useApi } from "../../../contexts";
import { ListBillingsOutput } from "../../../api";

interface IProps {
  billingId: string;
  checked: boolean;
}

export const CheckedCheckbox = ({ billingId, checked }: IProps) => {
  const { workspaceApi } = useApi();
  const queryClient = useQueryClient();
  const { mutate: onChange, isPending } = useMutation({
    mutationFn: async () =>
      await workspaceApi.setCheckedQuery({
        id: billingId,
        checked: !checked,
      }),
    onSuccess: (res) =>
      queryClient.setQueryData(
        ["billingsListing"],
        (prev: Array<ListBillingsOutput>) => {
          const index = prev.findIndex((n) => n.id === billingId);
          return index < 0
            ? prev
            : [
                ...prev.slice(0, index),
                { ...prev[index], ...res },
                ...prev.slice(index + 1),
              ];
        }
      ),
    //todo ARNAUD: à améliorer
  });
  if (isPending) return <CircularProgress size={16} />;
  return (
    <Checkbox
      disabled={isPending}
      checked={checked}
      onChange={() => onChange()}
    />
  );
};
