import {FormGroup} from "@angular/forms";

export const getFormFieldError = (formControl: FormGroup, field: string): string => {
  let message = '';
  const errors = formControl.controls[field].errors;
  if(errors) {
    Object.keys(errors).forEach(keyError => {
      switch (keyError) {
        case "required":
          message = "Required";
          break;
        case "pattern":
          message = "Value does not match the pattern";
          break;
        case "min":
          message = "The number less than acceptable";
          break;
        case "max":
          message = "The number greater than acceptable";
          break;
        case "minlength":
          message = `The string shorter than acceptable ${errors[keyError].requiredLength}`;
          break;
        case "maxlength":
          message = `The string longer than acceptable ${errors[keyError].requiredLength}`;
          break;
        case "match":
          message = "The field is not match";
          break;
        default:
          message = "";
      }
    });
  }

  return message;
}
