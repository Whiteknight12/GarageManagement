; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [312 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [618 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 67
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 66
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 107
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 231
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 265
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 79
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 142
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 9: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 306
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 123
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 185
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 101
	i32 101534019, ; 13: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 249
	i32 117431740, ; 14: System.Runtime.InteropServices => 0x6ffddbc => 106
	i32 120558881, ; 15: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 249
	i32 122350210, ; 16: System.Threading.Channels.dll => 0x74aea82 => 136
	i32 134690465, ; 17: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 269
	i32 142507400, ; 18: APIClient.dll => 0x87e7d88 => 307
	i32 142721839, ; 19: System.Net.WebHeaderCollection => 0x881c32f => 76
	i32 149972175, ; 20: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 123
	i32 159306688, ; 21: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 22: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 205
	i32 176265551, ; 23: System.ServiceProcess => 0xa81994f => 131
	i32 182336117, ; 24: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 251
	i32 184328833, ; 25: System.ValueTuple.dll => 0xafca281 => 148
	i32 195452805, ; 26: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 303
	i32 199333315, ; 27: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 304
	i32 205061960, ; 28: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 29: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 203
	i32 220171995, ; 30: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 31: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 225
	i32 230752869, ; 32: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 33: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 34: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 35: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 90
	i32 261689757, ; 36: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 208
	i32 276479776, ; 37: System.Threading.Timer.dll => 0x107abf20 => 144
	i32 278686392, ; 38: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 227
	i32 280482487, ; 39: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 224
	i32 280992041, ; 40: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 275
	i32 291076382, ; 41: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 42: System.Net.Ping.dll => 0x11d123fd => 68
	i32 317674968, ; 43: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 303
	i32 318968648, ; 44: Xamarin.AndroidX.Activity.dll => 0x13031348 => 194
	i32 321597661, ; 45: System.Numerics => 0x132b30dd => 82
	i32 336156722, ; 46: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 288
	i32 342366114, ; 47: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 226
	i32 356389973, ; 48: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 287
	i32 360082299, ; 49: System.ServiceModel.Web => 0x15766b7b => 130
	i32 367780167, ; 50: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 51: System.Transactions.Local => 0x1658bf94 => 146
	i32 375677976, ; 52: System.Net.ServicePoint.dll => 0x16646418 => 73
	i32 379916513, ; 53: System.Threading.Thread.dll => 0x16a510e1 => 142
	i32 385762202, ; 54: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 55: System.Threading.ThreadPool.dll => 0x1766c1f7 => 143
	i32 395744057, ; 56: _Microsoft.Android.Resource.Designer => 0x17969339 => 308
	i32 403441872, ; 57: WindowsBase => 0x180c08d0 => 162
	i32 435591531, ; 58: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 299
	i32 441335492, ; 59: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 209
	i32 442565967, ; 60: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 61: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 222
	i32 451504562, ; 62: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 124
	i32 456227837, ; 63: System.Web.HttpUtility.dll => 0x1b317bfd => 149
	i32 459347974, ; 64: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 112
	i32 465846621, ; 65: mscorlib => 0x1bc4415d => 163
	i32 469710990, ; 66: System.dll => 0x1bff388e => 161
	i32 476646585, ; 67: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 224
	i32 486930444, ; 68: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 237
	i32 498788369, ; 69: System.ObjectModel => 0x1dbae811 => 83
	i32 500358224, ; 70: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 286
	i32 503918385, ; 71: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 280
	i32 513247710, ; 72: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 179
	i32 526420162, ; 73: System.Transactions.dll => 0x1f6088c2 => 147
	i32 527452488, ; 74: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 269
	i32 530272170, ; 75: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 539058512, ; 76: Microsoft.Extensions.Logging => 0x20216150 => 175
	i32 540030774, ; 77: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 78: System.Runtime.Extensions => 0x2080b118 => 102
	i32 546455878, ; 79: System.Runtime.Serialization.Xml => 0x20924146 => 113
	i32 549171840, ; 80: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 81: Jsr305Binding => 0x213954e7 => 262
	i32 569601784, ; 82: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 260
	i32 577335427, ; 83: System.Security.Cryptography.Cng => 0x22697083 => 119
	i32 592146354, ; 84: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 294
	i32 601371474, ; 85: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 86: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 87: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 118
	i32 627609679, ; 88: Xamarin.AndroidX.CustomView => 0x2568904f => 214
	i32 627931235, ; 89: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 292
	i32 639843206, ; 90: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 220
	i32 643868501, ; 91: System.Net => 0x2660a755 => 80
	i32 662205335, ; 92: System.Text.Encodings.Web.dll => 0x27787397 => 188
	i32 663517072, ; 93: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 256
	i32 666292255, ; 94: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 201
	i32 672442732, ; 95: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 96: System.Net.Security => 0x28bdabca => 72
	i32 688181140, ; 97: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 274
	i32 690569205, ; 98: System.Xml.Linq.dll => 0x29293ff5 => 152
	i32 691348768, ; 99: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 271
	i32 693804605, ; 100: System.Windows => 0x295a9e3d => 151
	i32 699345723, ; 101: System.Reflection.Emit => 0x29af2b3b => 91
	i32 700284507, ; 102: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 266
	i32 700358131, ; 103: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 706645707, ; 104: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 289
	i32 709557578, ; 105: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 277
	i32 720511267, ; 106: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 270
	i32 722857257, ; 107: System.Runtime.Loader.dll => 0x2b15ed29 => 108
	i32 735137430, ; 108: System.Security.SecureString.dll => 0x2bd14e96 => 128
	i32 752232764, ; 109: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 110: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 191
	i32 759454413, ; 111: System.Net.Requests => 0x2d445acd => 71
	i32 762598435, ; 112: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 113: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 114: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 298
	i32 789151979, ; 115: Microsoft.Extensions.Options => 0x2f0980eb => 178
	i32 790371945, ; 116: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 215
	i32 804715423, ; 117: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 118: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 229
	i32 823281589, ; 119: System.Private.Uri.dll => 0x311247b5 => 85
	i32 830298997, ; 120: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 121: System.Xml.XPath.dll => 0x31a103c6 => 157
	i32 834051424, ; 122: System.Net.Quic => 0x31b69d60 => 70
	i32 843511501, ; 123: Xamarin.AndroidX.Print => 0x3246f6cd => 242
	i32 873119928, ; 124: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 125: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 126: System.Net.Http.Json => 0x3463c971 => 187
	i32 904024072, ; 127: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 128: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 926902833, ; 129: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 301
	i32 928116545, ; 130: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 265
	i32 952186615, ; 131: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 104
	i32 956575887, ; 132: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 270
	i32 966729478, ; 133: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 263
	i32 967690846, ; 134: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 226
	i32 975236339, ; 135: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 136: System.Xml.XDocument => 0x3a2aaa1d => 155
	i32 986514023, ; 137: System.Private.DataContractSerialization.dll => 0x3acd0267 => 84
	i32 987214855, ; 138: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 139: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 140: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 141: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 142: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 246
	i32 1019214401, ; 143: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 144: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 174
	i32 1029334545, ; 145: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 276
	i32 1031528504, ; 146: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 264
	i32 1035644815, ; 147: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 199
	i32 1036536393, ; 148: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 149: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 150: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 233
	i32 1067306892, ; 151: GoogleGson => 0x3f9dcf8c => 170
	i32 1082857460, ; 152: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 153: Xamarin.Kotlin.StdLib => 0x409e66d8 => 267
	i32 1098259244, ; 154: System => 0x41761b2c => 161
	i32 1118262833, ; 155: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 289
	i32 1121599056, ; 156: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 232
	i32 1127624469, ; 157: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 177
	i32 1149092582, ; 158: Xamarin.AndroidX.Window => 0x447dc2e6 => 259
	i32 1168523401, ; 159: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 295
	i32 1170634674, ; 160: System.Web.dll => 0x45c677b2 => 150
	i32 1175144683, ; 161: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 255
	i32 1178241025, ; 162: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 240
	i32 1203215381, ; 163: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 293
	i32 1204270330, ; 164: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 201
	i32 1208641965, ; 165: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 166: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1234928153, ; 167: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 291
	i32 1243150071, ; 168: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 260
	i32 1253011324, ; 169: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 170: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 275
	i32 1264511973, ; 171: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 250
	i32 1267360935, ; 172: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 254
	i32 1273260888, ; 173: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 206
	i32 1275534314, ; 174: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 271
	i32 1278448581, ; 175: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 198
	i32 1293217323, ; 176: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 217
	i32 1309188875, ; 177: System.Private.DataContractSerialization => 0x4e08a30b => 84
	i32 1322716291, ; 178: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 259
	i32 1324164729, ; 179: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 180: System.Runtime.Serialization.Json.dll => 0x4f97822f => 111
	i32 1364015309, ; 181: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 182: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 305
	i32 1376866003, ; 183: Xamarin.AndroidX.SavedState => 0x52114ed3 => 246
	i32 1379779777, ; 184: System.Resources.ResourceManager => 0x523dc4c1 => 98
	i32 1402170036, ; 185: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 186: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 210
	i32 1408764838, ; 187: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 110
	i32 1411638395, ; 188: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 100
	i32 1422545099, ; 189: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 101
	i32 1430672901, ; 190: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 273
	i32 1434145427, ; 191: System.Runtime.Handles => 0x557b5293 => 103
	i32 1435222561, ; 192: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 263
	i32 1439761251, ; 193: System.Net.Quic.dll => 0x55d10363 => 70
	i32 1452070440, ; 194: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 195: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 196: System.Runtime.Extensions.dll => 0x56e36530 => 102
	i32 1458022317, ; 197: System.Net.Security.dll => 0x56e7a7ad => 72
	i32 1461004990, ; 198: es\Microsoft.Maui.Controls.resources => 0x57152abe => 279
	i32 1461234159, ; 199: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 200: System.Security.Cryptography.OpenSsl => 0x57201017 => 122
	i32 1462112819, ; 201: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 202: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 200
	i32 1470490898, ; 203: Microsoft.Extensions.Primitives => 0x57a5e912 => 179
	i32 1479771757, ; 204: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 205: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 206: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 207: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 247
	i32 1493001747, ; 208: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 283
	i32 1514721132, ; 209: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 278
	i32 1536373174, ; 210: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 211: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 135
	i32 1543355203, ; 212: System.Reflection.Emit.dll => 0x5bfdbb43 => 91
	i32 1550322496, ; 213: System.Reflection.Extensions.dll => 0x5c680b40 => 92
	i32 1551623176, ; 214: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 298
	i32 1565862583, ; 215: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 216: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 138
	i32 1573704789, ; 217: System.Runtime.Serialization.Json => 0x5dccd455 => 111
	i32 1580037396, ; 218: System.Threading.Overlapped => 0x5e2d7514 => 137
	i32 1582372066, ; 219: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 216
	i32 1592978981, ; 220: System.Runtime.Serialization.dll => 0x5ef2ee25 => 114
	i32 1597949149, ; 221: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 264
	i32 1601112923, ; 222: System.Xml.Serialization => 0x5f6f0b5b => 154
	i32 1604827217, ; 223: System.Net.WebClient => 0x5fa7b851 => 75
	i32 1614005339, ; 224: APIClient => 0x6033c45b => 307
	i32 1618516317, ; 225: System.Net.WebSockets.Client.dll => 0x6078995d => 78
	i32 1622152042, ; 226: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 236
	i32 1622358360, ; 227: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 228: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 258
	i32 1635184631, ; 229: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 220
	i32 1636350590, ; 230: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 213
	i32 1639515021, ; 231: System.Net.Http.dll => 0x61b9038d => 63
	i32 1639986890, ; 232: System.Text.RegularExpressions => 0x61c036ca => 135
	i32 1641389582, ; 233: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 234: System.Runtime => 0x62c6282e => 115
	i32 1658241508, ; 235: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 252
	i32 1658251792, ; 236: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 261
	i32 1670060433, ; 237: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 208
	i32 1675553242, ; 238: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 239: System.Net.Primitives.dll => 0x63fca3d0 => 69
	i32 1678508291, ; 240: System.Net.WebSockets => 0x640c0103 => 79
	i32 1679769178, ; 241: System.Security.Cryptography => 0x641f3e5a => 125
	i32 1691477237, ; 242: System.Reflection.Metadata => 0x64d1e4f5 => 93
	i32 1696967625, ; 243: System.Security.Cryptography.Csp => 0x6525abc9 => 120
	i32 1698840827, ; 244: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 268
	i32 1701541528, ; 245: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1720223769, ; 246: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 229
	i32 1726116996, ; 247: System.Reflection.dll => 0x66e27484 => 96
	i32 1728033016, ; 248: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 249: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 204
	i32 1736233607, ; 250: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 296
	i32 1743415430, ; 251: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 274
	i32 1744735666, ; 252: System.Transactions.Local.dll => 0x67fe8db2 => 146
	i32 1746115085, ; 253: System.IO.Pipelines.dll => 0x68139a0d => 186
	i32 1746316138, ; 254: Mono.Android.Export => 0x6816ab6a => 166
	i32 1750313021, ; 255: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 256: System.Resources.Reader.dll => 0x68cc9d1e => 97
	i32 1763938596, ; 257: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 258: System.Reflection.Extensions => 0x6942234e => 92
	i32 1766324549, ; 259: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 251
	i32 1770582343, ; 260: Microsoft.Extensions.Logging.dll => 0x6988f147 => 175
	i32 1776026572, ; 261: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 262: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 263: Mono.Android.Runtime.dll => 0x6a216153 => 167
	i32 1782862114, ; 264: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 290
	i32 1788241197, ; 265: Xamarin.AndroidX.Fragment => 0x6a96652d => 222
	i32 1793755602, ; 266: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 282
	i32 1808609942, ; 267: Xamarin.AndroidX.Loader => 0x6bcd3296 => 236
	i32 1813058853, ; 268: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 267
	i32 1813201214, ; 269: Xamarin.Google.Android.Material => 0x6c13413e => 261
	i32 1818569960, ; 270: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 241
	i32 1818787751, ; 271: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 272: System.Text.Encoding.Extensions => 0x6cbab720 => 133
	i32 1824722060, ; 273: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 110
	i32 1828688058, ; 274: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 176
	i32 1842015223, ; 275: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 302
	i32 1847515442, ; 276: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 191
	i32 1853025655, ; 277: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 299
	i32 1858542181, ; 278: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 279: System.Reflection.Primitives => 0x6f7a29e4 => 94
	i32 1875935024, ; 280: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 281
	i32 1879696579, ; 281: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 282: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 202
	i32 1888955245, ; 283: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 284: System.Reflection.Metadata.dll => 0x70a66bdd => 93
	i32 1898237753, ; 285: System.Reflection.DispatchProxy => 0x7124cf39 => 88
	i32 1900610850, ; 286: System.Resources.ResourceManager.dll => 0x71490522 => 98
	i32 1910275211, ; 287: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 288: System.Private.Xml.Linq => 0x739bd4a8 => 86
	i32 1956758971, ; 289: System.Resources.Writer => 0x74a1c5bb => 99
	i32 1961813231, ; 290: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 248
	i32 1968388702, ; 291: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 171
	i32 1983156543, ; 292: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 268
	i32 1985761444, ; 293: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 193
	i32 2003115576, ; 294: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 278
	i32 2011961780, ; 295: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 296: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 233
	i32 2025202353, ; 297: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 273
	i32 2031763787, ; 298: Xamarin.Android.Glide => 0x791a414b => 190
	i32 2045470958, ; 299: System.Private.Xml => 0x79eb68ee => 87
	i32 2055257422, ; 300: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 228
	i32 2060060697, ; 301: System.Windows.dll => 0x7aca0819 => 151
	i32 2066184531, ; 302: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 277
	i32 2070888862, ; 303: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 304: System.Runtime.dll => 0x7bf8cdab => 115
	i32 2090596640, ; 305: System.Numerics.Vectors => 0x7c9bf920 => 81
	i32 2127167465, ; 306: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 307: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 308: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 159
	i32 2146852085, ; 309: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 310: Microsoft.Maui => 0x80bd55ad => 183
	i32 2169148018, ; 311: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 285
	i32 2181898931, ; 312: Microsoft.Extensions.Options.dll => 0x820d22b3 => 178
	i32 2192057212, ; 313: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 176
	i32 2193016926, ; 314: System.ObjectModel.dll => 0x82b6c85e => 83
	i32 2201107256, ; 315: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 272
	i32 2201231467, ; 316: System.Net.Http => 0x8334206b => 63
	i32 2207618523, ; 317: it\Microsoft.Maui.Controls.resources => 0x839595db => 287
	i32 2217644978, ; 318: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 255
	i32 2222056684, ; 319: System.Threading.Tasks.Parallel => 0x8471e4ec => 140
	i32 2244775296, ; 320: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 237
	i32 2252106437, ; 321: System.Xml.Serialization.dll => 0x863c6ac5 => 154
	i32 2256313426, ; 322: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 323: System.Security.AccessControl.dll => 0x8702d9a2 => 116
	i32 2266799131, ; 324: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 172
	i32 2267999099, ; 325: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 192
	i32 2270573516, ; 326: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 281
	i32 2279755925, ; 327: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 244
	i32 2293034957, ; 328: System.ServiceModel.Web.dll => 0x88acefcd => 130
	i32 2295906218, ; 329: System.Net.Sockets => 0x88d8bfaa => 74
	i32 2298471582, ; 330: System.Net.Mail => 0x88ffe49e => 65
	i32 2303942373, ; 331: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 291
	i32 2305521784, ; 332: System.Private.CoreLib.dll => 0x896b7878 => 169
	i32 2315684594, ; 333: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 196
	i32 2320631194, ; 334: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 140
	i32 2340441535, ; 335: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 105
	i32 2344264397, ; 336: System.ValueTuple => 0x8bbaa2cd => 148
	i32 2353062107, ; 337: System.Net.Primitives => 0x8c40e0db => 69
	i32 2368005991, ; 338: System.Xml.ReaderWriter.dll => 0x8d24e767 => 153
	i32 2371007202, ; 339: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 171
	i32 2378619854, ; 340: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 120
	i32 2383496789, ; 341: System.Security.Principal.Windows.dll => 0x8e114655 => 126
	i32 2395872292, ; 342: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 286
	i32 2401565422, ; 343: System.Web.HttpUtility => 0x8f24faee => 149
	i32 2403452196, ; 344: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 219
	i32 2421380589, ; 345: System.Threading.Tasks.Dataflow => 0x905355ed => 138
	i32 2423080555, ; 346: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 206
	i32 2427813419, ; 347: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 283
	i32 2435356389, ; 348: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 349: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 350: System.Text.Encoding.dll => 0x924edee6 => 134
	i32 2458678730, ; 351: System.Net.Sockets.dll => 0x928c75ca => 74
	i32 2459001652, ; 352: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465532216, ; 353: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 209
	i32 2471841756, ; 354: netstandard.dll => 0x93554fdc => 164
	i32 2475788418, ; 355: Java.Interop.dll => 0x93918882 => 165
	i32 2480646305, ; 356: Microsoft.Maui.Controls => 0x93dba8a1 => 181
	i32 2483903535, ; 357: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 358: System.Net.ServicePoint => 0x94147f61 => 73
	i32 2490993605, ; 359: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 360: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 361: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 231
	i32 2522472828, ; 362: Xamarin.Android.Glide.dll => 0x9659e17c => 190
	i32 2538310050, ; 363: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 90
	i32 2550873716, ; 364: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 284
	i32 2562349572, ; 365: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 366: System.Text.Encodings.Web => 0x9930ee42 => 188
	i32 2581783588, ; 367: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 232
	i32 2581819634, ; 368: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 254
	i32 2585220780, ; 369: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 133
	i32 2585805581, ; 370: System.Net.Ping => 0x9a20430d => 68
	i32 2589602615, ; 371: System.Threading.ThreadPool => 0x9a5a3337 => 143
	i32 2593496499, ; 372: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 293
	i32 2605712449, ; 373: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 272
	i32 2615233544, ; 374: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 223
	i32 2616218305, ; 375: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 177
	i32 2617129537, ; 376: System.Private.Xml.dll => 0x9bfe3a41 => 87
	i32 2618712057, ; 377: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 95
	i32 2620871830, ; 378: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 213
	i32 2624644809, ; 379: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 218
	i32 2626831493, ; 380: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 288
	i32 2627185994, ; 381: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 382: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 383: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 227
	i32 2663391936, ; 384: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 192
	i32 2663698177, ; 385: System.Runtime.Loader => 0x9ec4cf01 => 108
	i32 2664396074, ; 386: System.Xml.XDocument.dll => 0x9ecf752a => 155
	i32 2665622720, ; 387: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 388: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 389: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 113
	i32 2693849962, ; 390: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 391: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 252
	i32 2715334215, ; 392: System.Threading.Tasks.dll => 0xa1d8b647 => 141
	i32 2717744543, ; 393: System.Security.Claims => 0xa1fd7d9f => 117
	i32 2719963679, ; 394: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 119
	i32 2724373263, ; 395: System.Runtime.Numerics.dll => 0xa262a30f => 109
	i32 2732626843, ; 396: Xamarin.AndroidX.Activity => 0xa2e0939b => 194
	i32 2735172069, ; 397: System.Threading.Channels => 0xa30769e5 => 136
	i32 2737747696, ; 398: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 200
	i32 2740948882, ; 399: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 400: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 104
	i32 2752995522, ; 401: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 294
	i32 2758225723, ; 402: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 182
	i32 2764765095, ; 403: Microsoft.Maui.dll => 0xa4caf7a7 => 183
	i32 2765824710, ; 404: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 132
	i32 2770495804, ; 405: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 266
	i32 2778768386, ; 406: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 257
	i32 2779977773, ; 407: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 245
	i32 2785988530, ; 408: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 300
	i32 2788224221, ; 409: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 223
	i32 2801831435, ; 410: Microsoft.Maui.Graphics => 0xa7008e0b => 185
	i32 2803228030, ; 411: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 156
	i32 2806116107, ; 412: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 279
	i32 2810250172, ; 413: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 210
	i32 2819470561, ; 414: System.Xml.dll => 0xa80db4e1 => 160
	i32 2821205001, ; 415: System.ServiceProcess.dll => 0xa8282c09 => 131
	i32 2821294376, ; 416: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 245
	i32 2824502124, ; 417: System.Xml.XmlDocument => 0xa85a7b6c => 158
	i32 2831556043, ; 418: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 292
	i32 2838993487, ; 419: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 234
	i32 2849599387, ; 420: System.Threading.Overlapped.dll => 0xa9d96f9b => 137
	i32 2853208004, ; 421: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 257
	i32 2855708567, ; 422: Xamarin.AndroidX.Transition => 0xaa36a797 => 253
	i32 2861098320, ; 423: Mono.Android.Export.dll => 0xaa88e550 => 166
	i32 2861189240, ; 424: Microsoft.Maui.Essentials => 0xaa8a4878 => 184
	i32 2870099610, ; 425: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 195
	i32 2875164099, ; 426: Jsr305Binding.dll => 0xab5f85c3 => 262
	i32 2875220617, ; 427: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 428: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 221
	i32 2887636118, ; 429: System.Net.dll => 0xac1dd496 => 80
	i32 2899753641, ; 430: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 431: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 432: System.Reflection => 0xacf080de => 96
	i32 2905242038, ; 433: mscorlib.dll => 0xad2a79b6 => 163
	i32 2909740682, ; 434: System.Private.CoreLib => 0xad6f1e8a => 169
	i32 2916838712, ; 435: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 258
	i32 2919462931, ; 436: System.Numerics.Vectors.dll => 0xae037813 => 81
	i32 2921128767, ; 437: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 197
	i32 2936416060, ; 438: System.Resources.Reader => 0xaf06273c => 97
	i32 2940926066, ; 439: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 440: System.Xml.XPath.XDocument => 0xaf624531 => 156
	i32 2959614098, ; 441: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 442: System.Security.Principal.Windows => 0xb0ed41f3 => 126
	i32 2972252294, ; 443: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 118
	i32 2978675010, ; 444: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 217
	i32 2987532451, ; 445: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 248
	i32 2996846495, ; 446: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 230
	i32 3016983068, ; 447: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 250
	i32 3023353419, ; 448: WindowsBase.dll => 0xb434b64b => 162
	i32 3024354802, ; 449: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 225
	i32 3038032645, ; 450: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 308
	i32 3056245963, ; 451: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 247
	i32 3057625584, ; 452: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 238
	i32 3059408633, ; 453: Mono.Android.Runtime => 0xb65adef9 => 167
	i32 3059793426, ; 454: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 455: System.Threading.Tasks => 0xb755818f => 141
	i32 3077302341, ; 456: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 285
	i32 3090735792, ; 457: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 124
	i32 3099732863, ; 458: System.Security.Claims.dll => 0xb8c22b7f => 117
	i32 3103600923, ; 459: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 460: System.Runtime.Serialization => 0xb979e222 => 114
	i32 3121463068, ; 461: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 462: System.Threading.Tasks.Extensions => 0xba4127cb => 139
	i32 3132293585, ; 463: System.Security.AccessControl => 0xbab301d1 => 116
	i32 3147165239, ; 464: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 465: GoogleGson.dll => 0xbba64c02 => 170
	i32 3159123045, ; 466: System.Reflection.Primitives.dll => 0xbc4c6465 => 94
	i32 3160747431, ; 467: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 468: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 239
	i32 3192346100, ; 469: System.Security.SecureString => 0xbe4755f4 => 128
	i32 3192476961, ; 470: MAUIProject => 0xbe495521 => 0
	i32 3193515020, ; 471: System.Web => 0xbe592c0c => 150
	i32 3204380047, ; 472: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 473: System.Xml.XmlDocument.dll => 0xbf506931 => 158
	i32 3211777861, ; 474: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 216
	i32 3220365878, ; 475: System.Threading => 0xbff2e236 => 145
	i32 3226221578, ; 476: System.Runtime.Handles.dll => 0xc04c3c0a => 103
	i32 3251039220, ; 477: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 88
	i32 3258312781, ; 478: Xamarin.AndroidX.CardView => 0xc235e84d => 204
	i32 3265493905, ; 479: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 480: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 139
	i32 3277815716, ; 481: System.Resources.Writer.dll => 0xc35f7fa4 => 99
	i32 3279906254, ; 482: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 483: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 484: System.Security.Cryptography.Encoding => 0xc4251ff9 => 121
	i32 3299363146, ; 485: System.Text.Encoding => 0xc4a8494a => 134
	i32 3303498502, ; 486: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 487: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 280
	i32 3316684772, ; 488: System.Net.Requests.dll => 0xc5b097e4 => 71
	i32 3317135071, ; 489: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 214
	i32 3317144872, ; 490: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 491: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 202
	i32 3345895724, ; 492: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 243
	i32 3346324047, ; 493: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 240
	i32 3357674450, ; 494: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 297
	i32 3358260929, ; 495: System.Text.Json => 0xc82afec1 => 189
	i32 3362336904, ; 496: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 195
	i32 3362522851, ; 497: Xamarin.AndroidX.Core => 0xc86c06e3 => 211
	i32 3366347497, ; 498: Java.Interop => 0xc8a662e9 => 165
	i32 3374999561, ; 499: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 244
	i32 3381016424, ; 500: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 276
	i32 3395150330, ; 501: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 100
	i32 3403906625, ; 502: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 122
	i32 3405233483, ; 503: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 215
	i32 3428513518, ; 504: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 173
	i32 3429136800, ; 505: System.Xml => 0xcc6479a0 => 160
	i32 3430777524, ; 506: netstandard => 0xcc7d82b4 => 164
	i32 3441283291, ; 507: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 218
	i32 3445260447, ; 508: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 509: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 180
	i32 3463511458, ; 510: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 284
	i32 3471940407, ; 511: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 512: Mono.Android => 0xcf3163e6 => 168
	i32 3479583265, ; 513: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 297
	i32 3484440000, ; 514: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 296
	i32 3485117614, ; 515: System.Text.Json.dll => 0xcfbaacae => 189
	i32 3486566296, ; 516: System.Transactions => 0xcfd0c798 => 147
	i32 3493954962, ; 517: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 207
	i32 3509114376, ; 518: System.Xml.Linq => 0xd128d608 => 152
	i32 3515174580, ; 519: System.Security.dll => 0xd1854eb4 => 129
	i32 3530912306, ; 520: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 521: System.Net.HttpListener => 0xd2ff69f1 => 64
	i32 3560100363, ; 522: System.Threading.Timer => 0xd432d20b => 144
	i32 3570554715, ; 523: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3580758918, ; 524: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 304
	i32 3597029428, ; 525: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 193
	i32 3598340787, ; 526: System.Net.WebSockets.Client => 0xd67a52b3 => 78
	i32 3608519521, ; 527: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 528: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 105
	i32 3627220390, ; 529: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 242
	i32 3633644679, ; 530: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 197
	i32 3638274909, ; 531: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 532: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 228
	i32 3643446276, ; 533: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 301
	i32 3643854240, ; 534: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 239
	i32 3645089577, ; 535: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 536: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 172
	i32 3660523487, ; 537: System.Net.NetworkInformation => 0xda2f27df => 67
	i32 3672681054, ; 538: Mono.Android.dll => 0xdae8aa5e => 168
	i32 3682565725, ; 539: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 203
	i32 3684561358, ; 540: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 207
	i32 3697841164, ; 541: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 306
	i32 3700866549, ; 542: System.Net.WebProxy.dll => 0xdc96bdf5 => 77
	i32 3706696989, ; 543: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 212
	i32 3716563718, ; 544: System.Runtime.Intrinsics => 0xdd864306 => 107
	i32 3718780102, ; 545: Xamarin.AndroidX.Annotation => 0xdda814c6 => 196
	i32 3724971120, ; 546: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 238
	i32 3732100267, ; 547: System.Net.NameResolution => 0xde7354ab => 66
	i32 3737834244, ; 548: System.Net.Http.Json.dll => 0xdecad304 => 187
	i32 3748608112, ; 549: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 550: System.Xml.XPath => 0xdf9a7f42 => 157
	i32 3786282454, ; 551: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 205
	i32 3792276235, ; 552: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 553: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 180
	i32 3802395368, ; 554: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 555: System.Net.WebProxy => 0xe3a54a09 => 77
	i32 3823082795, ; 556: System.Security.Cryptography.dll => 0xe3df9d2b => 125
	i32 3829621856, ; 557: System.Numerics.dll => 0xe4436460 => 82
	i32 3841636137, ; 558: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 174
	i32 3844307129, ; 559: System.Net.Mail.dll => 0xe52378b9 => 65
	i32 3849253459, ; 560: System.Runtime.InteropServices.dll => 0xe56ef253 => 106
	i32 3870376305, ; 561: System.Net.HttpListener.dll => 0xe6b14171 => 64
	i32 3873536506, ; 562: System.Security.Principal => 0xe6e179fa => 127
	i32 3875112723, ; 563: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 121
	i32 3885497537, ; 564: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 76
	i32 3885922214, ; 565: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 253
	i32 3888767677, ; 566: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 243
	i32 3889960447, ; 567: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 305
	i32 3896106733, ; 568: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 569: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 211
	i32 3901907137, ; 570: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 571: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 572: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 256
	i32 3928044579, ; 573: System.Xml.ReaderWriter => 0xea213423 => 153
	i32 3930554604, ; 574: System.Security.Principal.dll => 0xea4780ec => 127
	i32 3931092270, ; 575: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 241
	i32 3945713374, ; 576: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 577: System.Text.Encoding.CodePages => 0xebac8bfe => 132
	i32 3955647286, ; 578: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 199
	i32 3959773229, ; 579: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 230
	i32 3980434154, ; 580: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 300
	i32 3987592930, ; 581: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 282
	i32 4003436829, ; 582: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 583: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 198
	i32 4023392905, ; 584: System.IO.Pipelines => 0xefd01a89 => 186
	i32 4025784931, ; 585: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 586: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 182
	i32 4051131400, ; 587: MAUIProject.dll => 0xf1775c08 => 0
	i32 4054681211, ; 588: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 89
	i32 4068434129, ; 589: System.Private.Xml.Linq.dll => 0xf27f60d1 => 86
	i32 4073602200, ; 590: System.Threading.dll => 0xf2ce3c98 => 145
	i32 4094352644, ; 591: Microsoft.Maui.Essentials.dll => 0xf40add04 => 184
	i32 4099507663, ; 592: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 593: System.Private.Uri => 0xf462c30d => 85
	i32 4101593132, ; 594: Xamarin.AndroidX.Emoji2 => 0xf479582c => 219
	i32 4102112229, ; 595: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 295
	i32 4125707920, ; 596: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 290
	i32 4126470640, ; 597: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 173
	i32 4127667938, ; 598: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 599: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 600: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 89
	i32 4150914736, ; 601: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 302
	i32 4151237749, ; 602: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 603: System.Xml.XmlSerializer => 0xf7e95c85 => 159
	i32 4161255271, ; 604: System.Reflection.TypeExtensions => 0xf807b767 => 95
	i32 4164802419, ; 605: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 606: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 112
	i32 4182413190, ; 607: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 235
	i32 4185676441, ; 608: System.Security => 0xf97c5a99 => 129
	i32 4196529839, ; 609: System.Net.WebClient.dll => 0xfa21f6af => 75
	i32 4213026141, ; 610: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4256097574, ; 611: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 212
	i32 4258378803, ; 612: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 234
	i32 4260525087, ; 613: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 614: Microsoft.Maui.Controls.dll => 0xfea12dee => 181
	i32 4274976490, ; 615: System.Runtime.Numerics => 0xfecef6ea => 109
	i32 4292120959, ; 616: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 235
	i32 4294763496 ; 617: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 221
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [618 x i32] [
	i32 67, ; 0
	i32 66, ; 1
	i32 107, ; 2
	i32 231, ; 3
	i32 265, ; 4
	i32 48, ; 5
	i32 79, ; 6
	i32 142, ; 7
	i32 30, ; 8
	i32 306, ; 9
	i32 123, ; 10
	i32 185, ; 11
	i32 101, ; 12
	i32 249, ; 13
	i32 106, ; 14
	i32 249, ; 15
	i32 136, ; 16
	i32 269, ; 17
	i32 307, ; 18
	i32 76, ; 19
	i32 123, ; 20
	i32 13, ; 21
	i32 205, ; 22
	i32 131, ; 23
	i32 251, ; 24
	i32 148, ; 25
	i32 303, ; 26
	i32 304, ; 27
	i32 18, ; 28
	i32 203, ; 29
	i32 26, ; 30
	i32 225, ; 31
	i32 1, ; 32
	i32 59, ; 33
	i32 42, ; 34
	i32 90, ; 35
	i32 208, ; 36
	i32 144, ; 37
	i32 227, ; 38
	i32 224, ; 39
	i32 275, ; 40
	i32 54, ; 41
	i32 68, ; 42
	i32 303, ; 43
	i32 194, ; 44
	i32 82, ; 45
	i32 288, ; 46
	i32 226, ; 47
	i32 287, ; 48
	i32 130, ; 49
	i32 55, ; 50
	i32 146, ; 51
	i32 73, ; 52
	i32 142, ; 53
	i32 62, ; 54
	i32 143, ; 55
	i32 308, ; 56
	i32 162, ; 57
	i32 299, ; 58
	i32 209, ; 59
	i32 12, ; 60
	i32 222, ; 61
	i32 124, ; 62
	i32 149, ; 63
	i32 112, ; 64
	i32 163, ; 65
	i32 161, ; 66
	i32 224, ; 67
	i32 237, ; 68
	i32 83, ; 69
	i32 286, ; 70
	i32 280, ; 71
	i32 179, ; 72
	i32 147, ; 73
	i32 269, ; 74
	i32 60, ; 75
	i32 175, ; 76
	i32 51, ; 77
	i32 102, ; 78
	i32 113, ; 79
	i32 40, ; 80
	i32 262, ; 81
	i32 260, ; 82
	i32 119, ; 83
	i32 294, ; 84
	i32 52, ; 85
	i32 44, ; 86
	i32 118, ; 87
	i32 214, ; 88
	i32 292, ; 89
	i32 220, ; 90
	i32 80, ; 91
	i32 188, ; 92
	i32 256, ; 93
	i32 201, ; 94
	i32 8, ; 95
	i32 72, ; 96
	i32 274, ; 97
	i32 152, ; 98
	i32 271, ; 99
	i32 151, ; 100
	i32 91, ; 101
	i32 266, ; 102
	i32 45, ; 103
	i32 289, ; 104
	i32 277, ; 105
	i32 270, ; 106
	i32 108, ; 107
	i32 128, ; 108
	i32 25, ; 109
	i32 191, ; 110
	i32 71, ; 111
	i32 55, ; 112
	i32 46, ; 113
	i32 298, ; 114
	i32 178, ; 115
	i32 215, ; 116
	i32 22, ; 117
	i32 229, ; 118
	i32 85, ; 119
	i32 43, ; 120
	i32 157, ; 121
	i32 70, ; 122
	i32 242, ; 123
	i32 3, ; 124
	i32 42, ; 125
	i32 187, ; 126
	i32 16, ; 127
	i32 53, ; 128
	i32 301, ; 129
	i32 265, ; 130
	i32 104, ; 131
	i32 270, ; 132
	i32 263, ; 133
	i32 226, ; 134
	i32 34, ; 135
	i32 155, ; 136
	i32 84, ; 137
	i32 32, ; 138
	i32 12, ; 139
	i32 51, ; 140
	i32 56, ; 141
	i32 246, ; 142
	i32 36, ; 143
	i32 174, ; 144
	i32 276, ; 145
	i32 264, ; 146
	i32 199, ; 147
	i32 35, ; 148
	i32 58, ; 149
	i32 233, ; 150
	i32 170, ; 151
	i32 17, ; 152
	i32 267, ; 153
	i32 161, ; 154
	i32 289, ; 155
	i32 232, ; 156
	i32 177, ; 157
	i32 259, ; 158
	i32 295, ; 159
	i32 150, ; 160
	i32 255, ; 161
	i32 240, ; 162
	i32 293, ; 163
	i32 201, ; 164
	i32 29, ; 165
	i32 52, ; 166
	i32 291, ; 167
	i32 260, ; 168
	i32 5, ; 169
	i32 275, ; 170
	i32 250, ; 171
	i32 254, ; 172
	i32 206, ; 173
	i32 271, ; 174
	i32 198, ; 175
	i32 217, ; 176
	i32 84, ; 177
	i32 259, ; 178
	i32 61, ; 179
	i32 111, ; 180
	i32 57, ; 181
	i32 305, ; 182
	i32 246, ; 183
	i32 98, ; 184
	i32 19, ; 185
	i32 210, ; 186
	i32 110, ; 187
	i32 100, ; 188
	i32 101, ; 189
	i32 273, ; 190
	i32 103, ; 191
	i32 263, ; 192
	i32 70, ; 193
	i32 38, ; 194
	i32 32, ; 195
	i32 102, ; 196
	i32 72, ; 197
	i32 279, ; 198
	i32 9, ; 199
	i32 122, ; 200
	i32 46, ; 201
	i32 200, ; 202
	i32 179, ; 203
	i32 9, ; 204
	i32 43, ; 205
	i32 4, ; 206
	i32 247, ; 207
	i32 283, ; 208
	i32 278, ; 209
	i32 31, ; 210
	i32 135, ; 211
	i32 91, ; 212
	i32 92, ; 213
	i32 298, ; 214
	i32 49, ; 215
	i32 138, ; 216
	i32 111, ; 217
	i32 137, ; 218
	i32 216, ; 219
	i32 114, ; 220
	i32 264, ; 221
	i32 154, ; 222
	i32 75, ; 223
	i32 307, ; 224
	i32 78, ; 225
	i32 236, ; 226
	i32 37, ; 227
	i32 258, ; 228
	i32 220, ; 229
	i32 213, ; 230
	i32 63, ; 231
	i32 135, ; 232
	i32 15, ; 233
	i32 115, ; 234
	i32 252, ; 235
	i32 261, ; 236
	i32 208, ; 237
	i32 48, ; 238
	i32 69, ; 239
	i32 79, ; 240
	i32 125, ; 241
	i32 93, ; 242
	i32 120, ; 243
	i32 268, ; 244
	i32 26, ; 245
	i32 229, ; 246
	i32 96, ; 247
	i32 28, ; 248
	i32 204, ; 249
	i32 296, ; 250
	i32 274, ; 251
	i32 146, ; 252
	i32 186, ; 253
	i32 166, ; 254
	i32 4, ; 255
	i32 97, ; 256
	i32 33, ; 257
	i32 92, ; 258
	i32 251, ; 259
	i32 175, ; 260
	i32 21, ; 261
	i32 41, ; 262
	i32 167, ; 263
	i32 290, ; 264
	i32 222, ; 265
	i32 282, ; 266
	i32 236, ; 267
	i32 267, ; 268
	i32 261, ; 269
	i32 241, ; 270
	i32 2, ; 271
	i32 133, ; 272
	i32 110, ; 273
	i32 176, ; 274
	i32 302, ; 275
	i32 191, ; 276
	i32 299, ; 277
	i32 58, ; 278
	i32 94, ; 279
	i32 281, ; 280
	i32 39, ; 281
	i32 202, ; 282
	i32 25, ; 283
	i32 93, ; 284
	i32 88, ; 285
	i32 98, ; 286
	i32 10, ; 287
	i32 86, ; 288
	i32 99, ; 289
	i32 248, ; 290
	i32 171, ; 291
	i32 268, ; 292
	i32 193, ; 293
	i32 278, ; 294
	i32 7, ; 295
	i32 233, ; 296
	i32 273, ; 297
	i32 190, ; 298
	i32 87, ; 299
	i32 228, ; 300
	i32 151, ; 301
	i32 277, ; 302
	i32 33, ; 303
	i32 115, ; 304
	i32 81, ; 305
	i32 20, ; 306
	i32 11, ; 307
	i32 159, ; 308
	i32 3, ; 309
	i32 183, ; 310
	i32 285, ; 311
	i32 178, ; 312
	i32 176, ; 313
	i32 83, ; 314
	i32 272, ; 315
	i32 63, ; 316
	i32 287, ; 317
	i32 255, ; 318
	i32 140, ; 319
	i32 237, ; 320
	i32 154, ; 321
	i32 41, ; 322
	i32 116, ; 323
	i32 172, ; 324
	i32 192, ; 325
	i32 281, ; 326
	i32 244, ; 327
	i32 130, ; 328
	i32 74, ; 329
	i32 65, ; 330
	i32 291, ; 331
	i32 169, ; 332
	i32 196, ; 333
	i32 140, ; 334
	i32 105, ; 335
	i32 148, ; 336
	i32 69, ; 337
	i32 153, ; 338
	i32 171, ; 339
	i32 120, ; 340
	i32 126, ; 341
	i32 286, ; 342
	i32 149, ; 343
	i32 219, ; 344
	i32 138, ; 345
	i32 206, ; 346
	i32 283, ; 347
	i32 20, ; 348
	i32 14, ; 349
	i32 134, ; 350
	i32 74, ; 351
	i32 59, ; 352
	i32 209, ; 353
	i32 164, ; 354
	i32 165, ; 355
	i32 181, ; 356
	i32 15, ; 357
	i32 73, ; 358
	i32 6, ; 359
	i32 23, ; 360
	i32 231, ; 361
	i32 190, ; 362
	i32 90, ; 363
	i32 284, ; 364
	i32 1, ; 365
	i32 188, ; 366
	i32 232, ; 367
	i32 254, ; 368
	i32 133, ; 369
	i32 68, ; 370
	i32 143, ; 371
	i32 293, ; 372
	i32 272, ; 373
	i32 223, ; 374
	i32 177, ; 375
	i32 87, ; 376
	i32 95, ; 377
	i32 213, ; 378
	i32 218, ; 379
	i32 288, ; 380
	i32 31, ; 381
	i32 45, ; 382
	i32 227, ; 383
	i32 192, ; 384
	i32 108, ; 385
	i32 155, ; 386
	i32 35, ; 387
	i32 22, ; 388
	i32 113, ; 389
	i32 57, ; 390
	i32 252, ; 391
	i32 141, ; 392
	i32 117, ; 393
	i32 119, ; 394
	i32 109, ; 395
	i32 194, ; 396
	i32 136, ; 397
	i32 200, ; 398
	i32 54, ; 399
	i32 104, ; 400
	i32 294, ; 401
	i32 182, ; 402
	i32 183, ; 403
	i32 132, ; 404
	i32 266, ; 405
	i32 257, ; 406
	i32 245, ; 407
	i32 300, ; 408
	i32 223, ; 409
	i32 185, ; 410
	i32 156, ; 411
	i32 279, ; 412
	i32 210, ; 413
	i32 160, ; 414
	i32 131, ; 415
	i32 245, ; 416
	i32 158, ; 417
	i32 292, ; 418
	i32 234, ; 419
	i32 137, ; 420
	i32 257, ; 421
	i32 253, ; 422
	i32 166, ; 423
	i32 184, ; 424
	i32 195, ; 425
	i32 262, ; 426
	i32 40, ; 427
	i32 221, ; 428
	i32 80, ; 429
	i32 56, ; 430
	i32 37, ; 431
	i32 96, ; 432
	i32 163, ; 433
	i32 169, ; 434
	i32 258, ; 435
	i32 81, ; 436
	i32 197, ; 437
	i32 97, ; 438
	i32 30, ; 439
	i32 156, ; 440
	i32 18, ; 441
	i32 126, ; 442
	i32 118, ; 443
	i32 217, ; 444
	i32 248, ; 445
	i32 230, ; 446
	i32 250, ; 447
	i32 162, ; 448
	i32 225, ; 449
	i32 308, ; 450
	i32 247, ; 451
	i32 238, ; 452
	i32 167, ; 453
	i32 16, ; 454
	i32 141, ; 455
	i32 285, ; 456
	i32 124, ; 457
	i32 117, ; 458
	i32 38, ; 459
	i32 114, ; 460
	i32 47, ; 461
	i32 139, ; 462
	i32 116, ; 463
	i32 34, ; 464
	i32 170, ; 465
	i32 94, ; 466
	i32 53, ; 467
	i32 239, ; 468
	i32 128, ; 469
	i32 0, ; 470
	i32 150, ; 471
	i32 24, ; 472
	i32 158, ; 473
	i32 216, ; 474
	i32 145, ; 475
	i32 103, ; 476
	i32 88, ; 477
	i32 204, ; 478
	i32 60, ; 479
	i32 139, ; 480
	i32 99, ; 481
	i32 5, ; 482
	i32 13, ; 483
	i32 121, ; 484
	i32 134, ; 485
	i32 28, ; 486
	i32 280, ; 487
	i32 71, ; 488
	i32 214, ; 489
	i32 24, ; 490
	i32 202, ; 491
	i32 243, ; 492
	i32 240, ; 493
	i32 297, ; 494
	i32 189, ; 495
	i32 195, ; 496
	i32 211, ; 497
	i32 165, ; 498
	i32 244, ; 499
	i32 276, ; 500
	i32 100, ; 501
	i32 122, ; 502
	i32 215, ; 503
	i32 173, ; 504
	i32 160, ; 505
	i32 164, ; 506
	i32 218, ; 507
	i32 39, ; 508
	i32 180, ; 509
	i32 284, ; 510
	i32 17, ; 511
	i32 168, ; 512
	i32 297, ; 513
	i32 296, ; 514
	i32 189, ; 515
	i32 147, ; 516
	i32 207, ; 517
	i32 152, ; 518
	i32 129, ; 519
	i32 19, ; 520
	i32 64, ; 521
	i32 144, ; 522
	i32 47, ; 523
	i32 304, ; 524
	i32 193, ; 525
	i32 78, ; 526
	i32 61, ; 527
	i32 105, ; 528
	i32 242, ; 529
	i32 197, ; 530
	i32 49, ; 531
	i32 228, ; 532
	i32 301, ; 533
	i32 239, ; 534
	i32 14, ; 535
	i32 172, ; 536
	i32 67, ; 537
	i32 168, ; 538
	i32 203, ; 539
	i32 207, ; 540
	i32 306, ; 541
	i32 77, ; 542
	i32 212, ; 543
	i32 107, ; 544
	i32 196, ; 545
	i32 238, ; 546
	i32 66, ; 547
	i32 187, ; 548
	i32 27, ; 549
	i32 157, ; 550
	i32 205, ; 551
	i32 10, ; 552
	i32 180, ; 553
	i32 11, ; 554
	i32 77, ; 555
	i32 125, ; 556
	i32 82, ; 557
	i32 174, ; 558
	i32 65, ; 559
	i32 106, ; 560
	i32 64, ; 561
	i32 127, ; 562
	i32 121, ; 563
	i32 76, ; 564
	i32 253, ; 565
	i32 243, ; 566
	i32 305, ; 567
	i32 8, ; 568
	i32 211, ; 569
	i32 2, ; 570
	i32 44, ; 571
	i32 256, ; 572
	i32 153, ; 573
	i32 127, ; 574
	i32 241, ; 575
	i32 23, ; 576
	i32 132, ; 577
	i32 199, ; 578
	i32 230, ; 579
	i32 300, ; 580
	i32 282, ; 581
	i32 29, ; 582
	i32 198, ; 583
	i32 186, ; 584
	i32 62, ; 585
	i32 182, ; 586
	i32 0, ; 587
	i32 89, ; 588
	i32 86, ; 589
	i32 145, ; 590
	i32 184, ; 591
	i32 36, ; 592
	i32 85, ; 593
	i32 219, ; 594
	i32 295, ; 595
	i32 290, ; 596
	i32 173, ; 597
	i32 50, ; 598
	i32 6, ; 599
	i32 89, ; 600
	i32 302, ; 601
	i32 21, ; 602
	i32 159, ; 603
	i32 95, ; 604
	i32 50, ; 605
	i32 112, ; 606
	i32 235, ; 607
	i32 129, ; 608
	i32 75, ; 609
	i32 27, ; 610
	i32 212, ; 611
	i32 234, ; 612
	i32 7, ; 613
	i32 181, ; 614
	i32 109, ; 615
	i32 235, ; 616
	i32 221 ; 617
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ a8cd27e430e55df3e3c1e3a43d35c11d9512a2db"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
