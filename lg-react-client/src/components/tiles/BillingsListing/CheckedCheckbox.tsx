import { Checkbox, CircularProgress } from "@mui/material";
import { useSetCheckedQuery } from "../../../queries/billings";

interface IProps {
  billingId: string;
  checked: boolean;
}

export const CheckedCheckbox = ({ billingId, checked }: IProps) => {
  const { mutate: onChange, isPending } = useSetCheckedQuery();
  return (
    <>
      <Checkbox
        disabled={isPending}
        size="small"
        checked={checked}
        onChange={() => onChange({ id: billingId, checked: !checked })}
      />
      {
        <CircularProgress
          style={{ visibility: isPending ? undefined : "hidden" }}
          size={16}
        />
      }
    </>
  );
};
