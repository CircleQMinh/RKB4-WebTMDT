import axios from "axios";

export async function upLoadImageToCloudinary(blob_string) {
  const response = await axios.post(
    "https://api.cloudinary.com/v1_1/dkmk9tdwx/image/upload",
    { file: blob_string, upload_preset: "v0q5hczm" }
  );
  return response;
}
