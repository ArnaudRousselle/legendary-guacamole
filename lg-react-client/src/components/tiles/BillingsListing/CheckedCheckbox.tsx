import { Checkbox, Spinner } from "@blueprintjs/core";
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
        checked={checked}
        onChange={() => onChange({ id: billingId, checked: !checked })}
      />
      {
        <Spinner
          style={{ visibility: isPending ? undefined : "hidden" }}
          size={16}
        />
      }
    </>
  );
};
