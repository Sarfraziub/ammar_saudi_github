using System.Reflection.PortableExecutable;
using MediatR;
using Newtonsoft.Json;

namespace Application.ClientOrderPayments.Commands.SendClientOrderPaymentPayload;

public class SendClientOrderPaymentPayloadCommand : IRequest<long>
{
	public long ClientOrderId { get; set; }
	public string Payload { get; set; }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class PtBillingDetails
{
    [JsonProperty("pt_name_billing")]
    public string PtNameBilling { get; set; }

    [JsonProperty("pt_email_billing")]
    public string PtEmailBilling { get; set; }

    [JsonProperty("pt_phone_billing")]
    public string PtPhoneBilling { get; set; }

    [JsonProperty("pt_address_billing")]
    public string PtAddressBilling { get; set; }

    [JsonProperty("pt_country_billing")]
    public string PtCountryBilling { get; set; }

    [JsonProperty("pt_city_billing")]
    public string PtCityBilling { get; set; }

    [JsonProperty("pt_state_billing")]
    public string PtStateBilling { get; set; }

    [JsonProperty("pt_zip_billing")]
    public string PtZipBilling { get; set; }
}

public class PtIosTheme
{
    [JsonProperty("pt_ios_logo")]
    public string PtIosLogo { get; set; }

    [JsonProperty("pt_ios_primary_color")]
    public object PtIosPrimaryColor { get; set; }

    [JsonProperty("pt_ios_primary_font_color")]
    public object PtIosPrimaryFontColor { get; set; }

    [JsonProperty("pt_ios_secondary_color")]
    public object PtIosSecondaryColor { get; set; }

    [JsonProperty("pt_ios_secondary_font_color")]
    public object PtIosSecondaryFontColor { get; set; }

    [JsonProperty("pt_ios_stroke_color")]
    public object PtIosStrokeColor { get; set; }

    [JsonProperty("pt_ios_button_color")]
    public object PtIosButtonColor { get; set; }

    [JsonProperty("pt_ios_button_font_color")]
    public object PtIosButtonFontColor { get; set; }

    [JsonProperty("pt_ios_title_font_color")]
    public object PtIosTitleFontColor { get; set; }

    [JsonProperty("pt_ios_background_color")]
    public object PtIosBackgroundColor { get; set; }

    [JsonProperty("pt_ios_placeholder_color")]
    public object PtIosPlaceholderColor { get; set; }

    [JsonProperty("pt_ios_primary_font")]
    public object PtIosPrimaryFont { get; set; }

    [JsonProperty("pt_ios_secondary_font")]
    public object PtIosSecondaryFont { get; set; }

    [JsonProperty("pt_ios_stroke_thinckness")]
    public object PtIosStrokeThinckness { get; set; }

    [JsonProperty("pt_ios_inputs_corner_radius")]
    public object PtIosInputsCornerRadius { get; set; }

    [JsonProperty("pt_ios_button_font")]
    public object PtIosButtonFont { get; set; }

    [JsonProperty("pt_ios_title_font")]
    public object PtIosTitleFont { get; set; }
}

public class PtShippingDetails
{
    [JsonProperty("pt_name_shipping")]
    public string PtNameShipping { get; set; }

    [JsonProperty("pt_email_shipping")]
    public string PtEmailShipping { get; set; }

    [JsonProperty("pt_phone_shipping")]
    public string PtPhoneShipping { get; set; }

    [JsonProperty("pt_address_shipping")]
    public string PtAddressShipping { get; set; }

    [JsonProperty("pt_country_shipping")]
    public string PtCountryShipping { get; set; }

    [JsonProperty("pt_city_shipping")]
    public string PtCityShipping { get; set; }

    [JsonProperty("pt_state_shipping")]
    public string PtStateShipping { get; set; }

    [JsonProperty("pt_zip_shipping")]
    public string PtZipShipping { get; set; }
}

public class PaytabsPayloadModel
{
    [JsonProperty("pt_profile_id")]
    public string PtProfileId { get; set; }

    [JsonProperty("pt_client_key")]
    public string PtClientKey { get; set; }

    [JsonProperty("pt_server_key")]
    public string PtServerKey { get; set; }

    [JsonProperty("pt_screen_title")]
    public string PtScreenTitle { get; set; }

    [JsonProperty("pt_merchant_name")]
    public string PtMerchantName { get; set; }

    [JsonProperty("pt_amount")]
    public double PtAmount { get; set; }

    [JsonProperty("pt_currency_code")]
    public string PtCurrencyCode { get; set; }

    [JsonProperty("pt_tokenise_type")]
    public string PtTokeniseType { get; set; }

    [JsonProperty("pt_token_format")]
    public object PtTokenFormat { get; set; }

    [JsonProperty("pt_token")]
    public object PtToken { get; set; }

    [JsonProperty("pt_transaction_reference")]
    public object PtTransactionReference { get; set; }

    [JsonProperty("pt_cart_id")]
    public string PtCartId { get; set; }

    [JsonProperty("pt_cart_description")]
    public string PtCartDescription { get; set; }

    [JsonProperty("pt_merchant_country_code")]
    public string PtMerchantCountryCode { get; set; }

    [JsonProperty("pt_samsung_pay_token")]
    public object PtSamsungPayToken { get; set; }

    [JsonProperty("pt_billing_details")]
    public PtBillingDetails PtBillingDetails { get; set; }

    [JsonProperty("pt_shipping_details")]
    public PtShippingDetails PtShippingDetails { get; set; }

    [JsonProperty("pt_language")]
    public string PtLanguage { get; set; }

    [JsonProperty("pt_show_billing_info")]
    public bool PtShowBillingInfo { get; set; }

    [JsonProperty("pt_show_shipping_info")]
    public object PtShowShippingInfo { get; set; }

    [JsonProperty("pt_force_validate_shipping")]
    public bool PtForceValidateShipping { get; set; }

    [JsonProperty("pt_ios_theme")]
    public PtIosTheme PtIosTheme { get; set; }

    [JsonProperty("pt_merchant_id")]
    public object PtMerchantId { get; set; }

    [JsonProperty("pt_simplify_apple_pay_validation")]
    public bool PtSimplifyApplePayValidation { get; set; }

    [JsonProperty("pt_hide_card_scanner")]
    public object PtHideCardScanner { get; set; }

    [JsonProperty("pt_transaction_class")]
    public object PtTransactionClass { get; set; }

    [JsonProperty("pt_transaction_type")]
    public object PtTransactionType { get; set; }

    [JsonProperty("pt_apms")]
    public string PtApms { get; set; }

    [JsonProperty("pt_link_billing_name")]
    public bool PtLinkBillingName { get; set; }

    [JsonProperty("pt_enable_zero_contacts")]
    public object PtEnableZeroContacts { get; set; }

    [JsonProperty("pt_is_digital_product")]
    public object PtIsDigitalProduct { get; set; }
}


