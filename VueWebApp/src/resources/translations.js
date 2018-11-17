export default {
  en: {
    form: {
      name: "Name",
      address: "Address",
      phone: "Phone number",
      email: "Email",
      age: "Age",
      submitted: "The form is submitted!",
      sentInfo: "Here is the info you sent: " + process.env.FORM_API_URL,
      return: "Return to the form",
      submit: "Submit",
      submitting: "Submitting",
      charactersLeft: "You have {charCount} character left. | You have {charCount} characters left.",
      types: {
        free: "Free trial subscription",
        starter: "Starter subscription (50 € / month)",
        enterprise: "Enterprise subscription (250 € / month)"
      }
    },
    error: {
      invalidFields: "Following fields have an invalid or a missing value:",
      general: "An error happened during submit.",
      generalMessage: "Form sending failed due to technical problems. Try again later.",
      fieldRequired: "{field} is required.",
      fieldInvalid: "{field} is invalid or missing.",
      fieldMaxLength: "{field} maximum characters exceeded."
    }
  }
};
