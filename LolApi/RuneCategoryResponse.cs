namespace BuildImporter
{
    public class RuneCategoryResponse
    {
        public RuneCategory[] Categories { get; set; }
    }

    public class RuneCategory
    {
        public int[] allowedSubStyles { get; set; }
        public Assetmap assetMap { get; set; }
        public string defaultPageName { get; set; }
        public int[] defaultPerks { get; set; }
        public int defaultSubStyle { get; set; }
        public string iconPath { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Slot[] slots { get; set; }
        public Substylebonu[] subStyleBonus { get; set; }
        public string tooltip { get; set; }
    }

    public class Assetmap
    {
        public string p8000_s0_k0 { get; set; }
        public string p8000_s0_k8005 { get; set; }
        public string p8000_s0_k8008 { get; set; }
        public string p8000_s0_k8010 { get; set; }
        public string p8000_s0_k8021 { get; set; }
        public string p8000_s8100_k0 { get; set; }
        public string p8000_s8100_k8005 { get; set; }
        public string p8000_s8100_k8008 { get; set; }
        public string p8000_s8100_k8010 { get; set; }
        public string p8000_s8100_k8021 { get; set; }
        public string p8000_s8200_k0 { get; set; }
        public string p8000_s8200_k8005 { get; set; }
        public string p8000_s8200_k8008 { get; set; }
        public string p8000_s8200_k8010 { get; set; }
        public string p8000_s8200_k8021 { get; set; }
        public string p8000_s8300_k0 { get; set; }
        public string p8000_s8300_k8005 { get; set; }
        public string p8000_s8300_k8008 { get; set; }
        public string p8000_s8300_k8010 { get; set; }
        public string p8000_s8300_k8021 { get; set; }
        public string p8000_s8400_k0 { get; set; }
        public string p8000_s8400_k8005 { get; set; }
        public string p8000_s8400_k8008 { get; set; }
        public string p8000_s8400_k8010 { get; set; }
        public string p8000_s8400_k8021 { get; set; }
        public string svg_icon { get; set; }
        public string svg_icon_16 { get; set; }
        public string p8100_s0_k0 { get; set; }
        public string p8100_s0_k8112 { get; set; }
        public string p8100_s0_k8124 { get; set; }
        public string p8100_s0_k8128 { get; set; }
        public string p8100_s0_k9923 { get; set; }
        public string p8100_s8000_k0 { get; set; }
        public string p8100_s8000_k8112 { get; set; }
        public string p8100_s8000_k8124 { get; set; }
        public string p8100_s8000_k8128 { get; set; }
        public string p8100_s8000_k9923 { get; set; }
        public string p8100_s8200_k0 { get; set; }
        public string p8100_s8200_k8112 { get; set; }
        public string p8100_s8200_k8124 { get; set; }
        public string p8100_s8200_k8128 { get; set; }
        public string p8100_s8200_k9923 { get; set; }
        public string p8100_s8300_k0 { get; set; }
        public string p8100_s8300_k8112 { get; set; }
        public string p8100_s8300_k8124 { get; set; }
        public string p8100_s8300_k8128 { get; set; }
        public string p8100_s8300_k9923 { get; set; }
        public string p8100_s8400_k0 { get; set; }
        public string p8100_s8400_k8112 { get; set; }
        public string p8100_s8400_k8124 { get; set; }
        public string p8100_s8400_k8128 { get; set; }
        public string p8100_s8400_k9923 { get; set; }
        public string p8200_s0_k0 { get; set; }
        public string p8200_s0_k8214 { get; set; }
        public string p8200_s0_k8229 { get; set; }
        public string p8200_s0_k8230 { get; set; }
        public string p8200_s8000_k0 { get; set; }
        public string p8200_s8000_k8214 { get; set; }
        public string p8200_s8000_k8229 { get; set; }
        public string p8200_s8000_k8230 { get; set; }
        public string p8200_s8100_k0 { get; set; }
        public string p8200_s8100_k8214 { get; set; }
        public string p8200_s8100_k8229 { get; set; }
        public string p8200_s8100_k8230 { get; set; }
        public string p8200_s8300_k0 { get; set; }
        public string p8200_s8300_k8214 { get; set; }
        public string p8200_s8300_k8229 { get; set; }
        public string p8200_s8300_k8230 { get; set; }
        public string p8200_s8400_k0 { get; set; }
        public string p8200_s8400_k8214 { get; set; }
        public string p8200_s8400_k8229 { get; set; }
        public string p8200_s8400_k8230 { get; set; }
        public string p8300_s0_k0 { get; set; }
        public string p8300_s0_k8351 { get; set; }
        public string p8300_s0_k8359 { get; set; }
        public string p8300_s0_k8360 { get; set; }
        public string p8300_s8000_k0 { get; set; }
        public string p8300_s8000_k8351 { get; set; }
        public string p8300_s8000_k8359 { get; set; }
        public string p8300_s8000_k8360 { get; set; }
        public string p8300_s8100_k0 { get; set; }
        public string p8300_s8100_k8351 { get; set; }
        public string p8300_s8100_k8359 { get; set; }
        public string p8300_s8100_k8360 { get; set; }
        public string p8300_s8200_k0 { get; set; }
        public string p8300_s8200_k8351 { get; set; }
        public string p8300_s8200_k8359 { get; set; }
        public string p8300_s8200_k8360 { get; set; }
        public string p8300_s8400_k0 { get; set; }
        public string p8300_s8400_k8351 { get; set; }
        public string p8300_s8400_k8359 { get; set; }
        public string p8300_s8400_k8360 { get; set; }
        public string p8400_s0_k0 { get; set; }
        public string p8400_s0_k8437 { get; set; }
        public string p8400_s0_k8439 { get; set; }
        public string p8400_s0_k8465 { get; set; }
        public string p8400_s8000_k0 { get; set; }
        public string p8400_s8000_k8437 { get; set; }
        public string p8400_s8000_k8439 { get; set; }
        public string p8400_s8000_k8465 { get; set; }
        public string p8400_s8100_k0 { get; set; }
        public string p8400_s8100_k8437 { get; set; }
        public string p8400_s8100_k8439 { get; set; }
        public string p8400_s8100_k8465 { get; set; }
        public string p8400_s8200_k0 { get; set; }
        public string p8400_s8200_k8437 { get; set; }
        public string p8400_s8200_k8439 { get; set; }
        public string p8400_s8200_k8465 { get; set; }
        public string p8400_s8300_k0 { get; set; }
        public string p8400_s8300_k8437 { get; set; }
        public string p8400_s8300_k8439 { get; set; }
        public string p8400_s8300_k8465 { get; set; }
    }

    public class Slot
    {
        public int[] perks { get; set; }
        public string slotLabel { get; set; }
        public string type { get; set; }
    }

    public class Substylebonu
    {
        public int perkId { get; set; }
        public int styleId { get; set; }
    }
}